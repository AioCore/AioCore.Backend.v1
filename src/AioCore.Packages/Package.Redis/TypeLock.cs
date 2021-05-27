using NeoSmart.AsyncLock;

namespace Package.Redis
{
    internal class TypeLock<T>
    {
        public static AsyncLock Lock = new AsyncLock();
    }
}
