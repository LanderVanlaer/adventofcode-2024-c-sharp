var list = new LinkedList<long>();

using (StreamReader file = new(args[0]))
{
    var line = await file.ReadLineAsync();

    foreach (var s in line!.Split(' ').Select(long.Parse)) list.AddLast(s);
}

for (var i = 0; i < 25; i++)
{
    var node = list.First;

    while (node != null)
    {
        if (node.Value == 0)
        {
            node.ValueRef = 1;
        }
        else
        {
            var log10 = (long)Math.Log10(node.Value);

            if (log10 % 2 == 1)
            {
                var half = (long)Math.Pow(10, (double)(log10 + 1) / 2);
                list.AddBefore(node, node.Value / half);
                node.ValueRef = node.Value % half;
            }
            else
            {
                node.ValueRef *= 2024;
            }
        }

        node = node.Next;
    }
}


Console.WriteLine(list.Count);

// If the stone is engraved with the number 0, it is replaced by a stone engraved with the number 1.
// If the stone is engraved with a number that has an even number of digits, it is replaced by two stones. The left half of the digits are engraved on the new left stone, and the right half of the digits are engraved on the new right stone. (The new numbers don't keep extra leading zeroes: 1000 would become stones 10 and 0.)
// If none of the other rules apply, the stone is replaced by a new stone; the old stone's number multiplied by 2024 is engraved on the new stone.