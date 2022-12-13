using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode3;

public struct Item : IComparable<Item>, IEquatable<Item>
{
    public char Representation { get; set; }

    public int Value => CalculateValue();

    public Item(char representation)
    {
        Representation = representation;
    }

    private int CalculateValue()
    {
        var repr = Representation;

        if (repr >= 'a' && repr <= 'z')
        {
            return repr - 'a' + 1;
        }

        if (repr >= 'A' && repr <= 'Z')
        {
            return repr - 'A' + 27;
        }

        throw new Exception($"invalid representation: '{repr}'");
    }

    public int CompareTo(Item other)
    {
        return Value.CompareTo(other.Value);
    }

    public bool Equals(Item other)
    {
        return Value == other.Value;
    }

    public override bool Equals([NotNullWhen(returnValue: true)] object? obj)
    {
        if (obj == null) return false;
        if (obj is not Item) return false;
        return Equals(obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return $"'{Representation}' ({Value})";
    }
}
