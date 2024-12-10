using _09_1;

var list = new LinkedList<Block>();
using (StreamReader file = new(args[0]))
{
    var line = await file.ReadLineAsync();
    if (line is null) throw new Exception("Invalid input");

    short i = 0;
    foreach (var length in line.Select(c => c - '0'))
    {
        if (length is < 0 or > 9) throw new Exception("Invalid input");

        if (length == 0)
        {
            ++i;
            continue;
        }

        var isEmptySpace = i % 2 == 1;

        list.AddLast(new Block { Length = (ushort)length, Id = (short)(isEmptySpace ? -1 : i / 2) });

        i++;
    }
}

var node = list.First;

while (node is not null)
{
    if (node.ValueRef.Id >= 0)
    {
        node = node.Next;
        if (node is null) Console.WriteLine("NULL");

        continue;
    }

    var totalLength = node.ValueRef.Length;

    while (list.Last!.ValueRef.Id < 0) list.RemoveLast();

    var lastNode = list.Last!;

    if (totalLength > lastNode.ValueRef.Length)
    {
        list.AddBefore(node, lastNode.ValueRef);
        list.RemoveLast();
        node.ValueRef.Length -= lastNode.ValueRef.Length;
    }
    else if (totalLength < lastNode.ValueRef.Length)
    {
        lastNode.ValueRef.Length -= totalLength;
        node.ValueRef.Id = lastNode.ValueRef.Id;
        node = node.Next;
    }
    else
    {
        node.ValueRef.Id = lastNode.ValueRef.Id;
        list.RemoveLast();
        node = node.Next;
    }
}

var checksum = 0L;

var count = 0;
foreach (var block in list)
    for (var i = 0; i < block.Length; i++)
        checksum += (long)block.Id * count++;

Console.WriteLine(checksum);