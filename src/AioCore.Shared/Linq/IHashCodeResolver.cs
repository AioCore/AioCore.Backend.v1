namespace AioCore.Shared.Linq
{
    internal interface IHashCodeResolver<in T>
    {
        int GetHashCodeFor(T obj);
    }
}