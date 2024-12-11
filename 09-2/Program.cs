using _09_2;

var list = new LinkedList<Block>();
using (StreamReader file = new(args[0]))
{
    var line = await file.ReadLineAsync();
    if (line is null) throw new Exception("Invalid input");

    short i = 0;
    foreach (var length in line.Select(c => c - '0'))
    {
        switch (length)
        {
            case < 0 or > 9:
                throw new Exception("Invalid input");
            case 0:
                ++i;
                continue;
        }

        var isEmptySpace = i % 2 == 1;

        list.AddLast(new Block { Length = (ushort)length, Id = (short)(isEmptySpace ? -1 : i / 2) });

        i++;
    }
}

var node = list.Last;

while (node is not null)
{
    var previous = node.Previous;

    if (node.ValueRef.Id < 0)
    {
        node = previous;
        continue;
    }

    var necessaryLength = node.ValueRef.Length;

    var firstOfAtLeastSameSize = list.First;

    while (firstOfAtLeastSameSize is not null
           && firstOfAtLeastSameSize.ValueRef.Id != node.ValueRef.Id
           && (firstOfAtLeastSameSize.ValueRef.Length < necessaryLength
               || firstOfAtLeastSameSize.ValueRef.Id >= 0))
        firstOfAtLeastSameSize = firstOfAtLeastSameSize.Next;

    if (firstOfAtLeastSameSize is null || firstOfAtLeastSameSize.ValueRef.Id == node.ValueRef.Id)
    {
        node = previous;
        continue;
    }

    if (necessaryLength < firstOfAtLeastSameSize.ValueRef.Length)
    {
        firstOfAtLeastSameSize.ValueRef.Length -= necessaryLength;
        list.AddBefore(firstOfAtLeastSameSize, node.ValueRef);
    }
    else
    {
        // equal
        firstOfAtLeastSameSize.ValueRef.Id = node.ValueRef.Id;
    }

    node.ValueRef.Id = -1;
    node = previous;
}

var checksum = 0L;

var count = 0;

foreach (var block in list)
    for (var i = 0; i < block.Length; i++)
    {
        if (block.Id > 0) checksum += (long)block.Id * count;

        ++count;
    }

Console.WriteLine(checksum);