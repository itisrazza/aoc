namespace AdventOfCode3;

public class Rucksack
{
    public List<Item> Items { get; }

    public IEnumerable<Item> FirstCompartment => Items.Take(Items.Count / 2);

    public IEnumerable<Item> SecondCompartment => Items.TakeLast(Items.Count / 2);

    public Rucksack(string items)
    {
        Items = new(items.Select(c => new Item(c)));
        if (Items.Count % 2 != 0)
        {
            throw new ArgumentException("The number of items must be even");
        }
    }

    public ISet<Item> ItemsInBothCompartments
    {
        get
        {
            var thisCommonItems = new HashSet<Item>();

            foreach (var item in FirstCompartment)
            {
                if (SecondCompartment.Contains(item))
                {
                    thisCommonItems.Add(item);
                }
            }

            foreach (var item in SecondCompartment)
            {
                if (FirstCompartment.Contains(item))
                {
                    thisCommonItems.Add(item);
                }
            }

            return thisCommonItems;
        }
    }
}

internal static class RucksackExtensions
{
    public static string AsContents(this IEnumerable<Item> items)
    {
        return items.Aggregate("", (acc, val) => acc + val.Representation);
    }

}
