using _05_1;

var conditionsList = new List<Condition>();
var validationLists = new List<List<int>>();

using (StreamReader file = new(args[0]))
{
    while (await file.ReadLineAsync() is { } line)
    {
        if (line.Length == 0) break;

        var before = int.Parse(line[..2]);
        var after = int.Parse(line[3..5]);

        conditionsList.Add(new Condition { Before = before, After = after });
    }

    while (await file.ReadLineAsync() is { } line) validationLists.Add(line.Split(',').Select(int.Parse).ToList());
}

var sum = 0;

foreach (var validationList in validationLists)
{
    var comesFrom = new Dictionary<int, List<int>>();
    var pointsTo = new Dictionary<int, List<int>>();

    foreach (var condition in conditionsList.Where(condition =>
                 validationList.Contains(condition.Before) && validationList.Contains(condition.After)))
    {
        if (!comesFrom.ContainsKey(condition.Before))
            comesFrom[condition.Before] = [];
        comesFrom[condition.Before].Add(condition.After);

        if (!pointsTo.ContainsKey(condition.After))
            pointsTo[condition.After] = [];
        pointsTo[condition.After].Add(condition.Before);
    }

    var conditionList = new List<int>(comesFrom.Count);

    var candidates = comesFrom.Keys.ToList();

    while (candidates.Count > 1)
    {
        // First find the element where no other element points to
        var current = candidates.First(x => !pointsTo.TryGetValue(x, out var value) || value.Count == 0);
        conditionList.Add(current);

        pointsTo.Remove(current);
        candidates = comesFrom[current];

        foreach (var candidate in candidates) pointsTo[candidate].Remove(current);
    }

    conditionList.Add(candidates[0]);

    var areSame = conditionList.SequenceEqual(validationList);

    if (areSame) sum += conditionList[conditionList.Count / 2];
}

Console.WriteLine(sum);