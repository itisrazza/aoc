using AdventOfCode3;

if (args.Length < 1)
{
    Console.Error.WriteLine("Usage: AdventOfCode3.exe <input>");
    Environment.Exit(1);
}

using var input = new StreamReader(File.Open(args[0], FileMode.Open));
var elfCommonItems = new List<Item>();
var groupCommonItems = new List<Item>();

Console.WriteLine("== Rucksack Loading ==");
string? line;
var i = 0;
var groupRucksacks = new List<Rucksack>();
while ((line = input.ReadLine()) != null)
{
    var rucksack = new Rucksack(line);
    Console.WriteLine($"rucksack: {rucksack.Items.AsContents()}");
    Console.WriteLine($"   first: {rucksack.FirstCompartment.AsContents()}");
    Console.WriteLine($"  second: {rucksack.SecondCompartment.AsContents()}");

    elfCommonItems.AddRange(rucksack.ItemsInBothCompartments);

    groupRucksacks.Add(rucksack);
    if (groupRucksacks.Count == 3)
    {
        var commonInAllThree = new HashSet<Item>();

        foreach (var item in groupRucksacks[0].Items)
        {
            if (groupRucksacks[1].Items.Contains(item) && groupRucksacks[2].Items.Contains(item))
            {
                commonInAllThree.Add(item);
            }
        }

        groupCommonItems.AddRange(commonInAllThree);
        groupRucksacks.Clear();
    }

    i++;
    Console.WriteLine();
}

Console.WriteLine();
Console.WriteLine("== Common Items ==");
foreach (var item in elfCommonItems.Order())
{
    Console.WriteLine(item);
}

Console.WriteLine();
Console.WriteLine("== Result ==");
Console.WriteLine($"Part 1: {elfCommonItems.Sum(a => a.Value)}");
Console.WriteLine($"Part 2: {groupCommonItems.Sum(a => a.Value)}");
