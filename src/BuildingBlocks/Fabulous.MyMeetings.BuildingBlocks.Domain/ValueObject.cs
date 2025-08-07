using System.Reflection;

namespace Fabulous.MyMeetings.BuildingBlocks.Domain;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public bool Equals(ValueObject? other)
    {
        return !ReferenceEquals(null, other) &&
               GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null)) return false;
        return ReferenceEquals(left, right) || left!.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }

    // Override == and != operators
    public static bool operator ==(ValueObject a, ValueObject b)
    {
        return EqualOperator(a, b);
    }

    public static bool operator !=(ValueObject a, ValueObject b)
    {
        return NotEqualOperator(a, b);
    }

    // Override Equals and GetHashCode
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ValueObject)obj);
    }

    public override int GetHashCode()
    {
        // Use a hash code combiner to generate a hash code based on all properties
        return HashCode.Combine(GetEqualityComponents());
    }

    // Get all properties or fields that contribute to equality
    protected virtual IEnumerable<object?> GetEqualityComponents()
    {
        foreach (var propertyInfo in GetProperties())
            yield return propertyInfo.GetValue(this);

        foreach (var fieldInfo in GetFields())
            yield return fieldInfo.GetValue(this);
    }

    private IEnumerable<PropertyInfo> GetProperties()
    {
        return GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    }

    private IEnumerable<FieldInfo> GetFields()
    {
        return GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
    }

    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
            throw new BusinessRuleValidationException(rule);
    }
}