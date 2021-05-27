namespace Package.Redis
{
    internal sealed class NullableObject<T>
    {
        public T Value { get; }

        public NullableObject(T value)
        {
            Value = value;
        }

        public static implicit operator T(NullableObject<T> nullableObject)
        {
            return nullableObject != null ? nullableObject.Value : default;
        }

        public static implicit operator NullableObject<T>(T objectValue)
        {
            return new NullableObject<T>(objectValue);
        }
    }
}
