using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Package.Redis
{
    public interface ICacheManager
    {
        bool Delete(string key);

        Task<bool> DeleteAsync(string key);

        T Get<T>(string key);

        Task<T> GetAsync<T>(string key);

        T GetOrSet<T>(string key, Func<T> data, int expire = 0, CacheModes cacheMode = CacheModes.Sliding);

        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> data, int expire = 0, CacheModes cacheMode = CacheModes.Sliding);

        T HashGet<T>(string key, string field);

        IEnumerable<T> HashGetAll<T>(string key);

        Task<T> HashGetAsync<T>(string key, string field);

        T HashGetOrSet<T>(string key, string field, Func<T> data, DateTime expiry);

        Task<T> HashGetOrSetAsync<T>(string key, string field, Func<Task<T>> data, DateTime expiry);

        void HashSet<T>(string key, string field, T data, DateTime expiry);

        void HashSetAll(string key, HashEntry[] entries, DateTime expire);

        Task HashSetAsync<T>(string key, string field, T data, DateTime expiry);

        bool KeyExpire(string key, DateTime expiry);

        Task<bool> KeyExpireAsync(string key, DateTime expiry);

        void Remove(string key);

        Task RemoveAsync(string key);

        void Set<T>(string key, T data, int expire = 0, CacheModes cacheMode = CacheModes.Sliding);

        Task SetAsync<T>(string key, T data, int expire = 0, CacheModes cacheMode = CacheModes.Sliding);
    }
}