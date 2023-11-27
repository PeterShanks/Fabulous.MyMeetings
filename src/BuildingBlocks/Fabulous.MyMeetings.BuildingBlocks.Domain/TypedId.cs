namespace Fabulous.MyMeetings.BuildingBlocks.Domain
{
    public abstract class TypedId: IEquatable<TypedId>
    {
        public Guid Value { get; }

        /// <exception cref="InvalidOperationException">Id value cannot be empty</exception>
        protected TypedId(Guid value)
        {
            if (value.Equals(Guid.Empty))
                throw new InvalidOperationException("Id value cannot be empty");

            Value = value;
        }

        public bool Equals(TypedId? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            return Value.Equals(other.Value);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TypedId)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        protected static bool EqualOperator(TypedId? left, TypedId? right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }
            return ReferenceEquals(left, right) || left!.Equals(right);
        }

        protected static bool NotEqualOperator(TypedId? left, TypedId? right)
        {
            return !(EqualOperator(left, right));
        }

        public static bool operator ==(TypedId? left, TypedId? right)
        {
            return EqualOperator(left, right);
        }

        public static bool operator !=(TypedId? left, TypedId? right)
        {
            return NotEqualOperator(left, right);
        }

        public static implicit operator Guid(TypedId id)
            => id.Value;
    }
}
