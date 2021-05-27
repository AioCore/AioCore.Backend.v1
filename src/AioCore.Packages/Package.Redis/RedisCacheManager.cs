using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Package.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly IConfiguration _configuration;

        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        private ConnectionMultiplexer Connection
        {
            get
            {
                return _lazyConnection.Value;
            }
        }

        private IDatabase CacheDb
        {
            get
            {
                return Connection.GetDatabase();
            }
        }

        public RedisCacheManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(_configuration["Redis"]));
        }

        public bool Delete(string key)
        {
            return CacheDb.KeyDelete(key);
        }

        public Task<bool> DeleteAsync(string key)
        {
            return CacheDb.KeyDeleteAsync(key);
        }

        public bool KeyExpire(string key, DateTime expiry)
        {
            return CacheDb.KeyExpire(key, expiry);
        }

        public async Task<bool> KeyExpireAsync(string key, DateTime expiry)
        {
            return await CacheDb.KeyExpireAsync(key, expiry);
        }

        public T Get<T>(string key)
        {
            if (CacheDb == null || !CacheDb.Multiplexer.IsConnected)
            {
                return default;
            }

            return Cast<T>(CacheDb.StringGet(key));
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (CacheDb == null || !CacheDb.Multiplexer.IsConnected)
            {
                return default;
            }

            return Cast<T>(await CacheDb.StringGetAsync(key));
        }

        public void Set<T>(string key, T data, int expire = 0, CacheModes cacheMode = CacheModes.Sliding)
        {
            if (CacheDb == null || !CacheDb.Multiplexer.IsConnected)
            {
                return;
            }
            if (cacheMode == CacheModes.Sliding)
            {
                TimeSpan? expiry = null;
                if (expire > 0)
                {
                    expiry = TimeSpan.FromSeconds(expire);
                }
                CacheDb.StringSet(key, Cast(data), expiry, flags: CommandFlags.FireAndForget);
            }
            else
            {
                var expireAt = DateTime.Now.AddSeconds(expire);
                CacheDb.StringSet(key, Cast(data));
                CacheDb.KeyExpire(key, expireAt, flags: CommandFlags.FireAndForget);
            }
        }

        public async Task SetAsync<T>(string key, T data, int expire = 0, CacheModes cacheMode = CacheModes.Sliding)
        {
            if (CacheDb == null || !CacheDb.Multiplexer.IsConnected)
            {
                return;
            }
            if (cacheMode == CacheModes.Sliding)
            {
                TimeSpan? expiry = null;
                if (expire > 0)
                {
                    expiry = TimeSpan.FromSeconds(expire);
                }
                await CacheDb.StringSetAsync(key, Cast(data), expiry, flags: CommandFlags.FireAndForget);
            }
            else
            {
                var expireAt = DateTime.Now.AddSeconds(expire);
                await CacheDb.StringSetAsync(key, Cast(data));
                await CacheDb.KeyExpireAsync(key, expireAt, flags: CommandFlags.FireAndForget);
            }
        }

        public void Remove(string key)
        {
            CacheDb.KeyDelete(key);
        }

        public async Task RemoveAsync(string key)
        {
            await CacheDb.KeyDeleteAsync(key);
        }

        public async Task HashSetAsync<T>(string key, string field, T data, DateTime expiry)
        {
            if (CacheDb == null || !CacheDb.Multiplexer.IsConnected)
            {
                return;
            }

            await CacheDb.HashSetAsync(key, field, Cast(data));
            var ttl = CacheDb.KeyTimeToLive(key);
            if (!ttl.HasValue || ttl.Value.TotalSeconds < 0)
            {
                await CacheDb.KeyExpireAsync(key, expiry);
            }
        }

        public void HashSetAll(string key, HashEntry[] entries, DateTime expire)
        {
            CacheDb.HashSet(key, entries);
            CacheDb.KeyExpire(key, expire);
        }

        public async Task<T> HashGetAsync<T>(string key, string field)
        {
            return Cast<T>(await CacheDb.HashGetAsync(key, field));
        }

        public T HashGet<T>(string key, string field)
        {
            return Cast<T>(CacheDb.HashGet(key, field));
        }

        public IEnumerable<T> HashGetAll<T>(string key)
        {
            return CacheDb.HashGetAll(key)
                ?.Select(t => (T)Cast<T>(t.Value))
                ?.ToList();
        }

        public void HashSet<T>(string key, string field, T data, DateTime expiry)
        {
            if (CacheDb == null || !CacheDb.Multiplexer.IsConnected)
            {
                return;
            }

            CacheDb.HashSet(key, field, Cast(data));
            var ttl = CacheDb.KeyTimeToLive(key);
            if (!ttl.HasValue || ttl.Value.TotalSeconds < 0)
            {
                CacheDb.KeyExpire(key, expiry);
            }
        }

        public T GetOrSet<T>(string key, Func<T> data, int expire = 0, CacheModes cacheMode = CacheModes.Sliding)
        {
            T result = Get<T>(key);
            if (IsEquals(result, default))
            {
                using (TypeLock<T>.Lock.Lock())
                {
                    result = Get<T>(key);
                    if (!IsEquals(result, default)) return result;
                    result = data();
                    Set(key, result, expire, cacheMode);
                }
            }
            return result;
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> data, int expire = 0, CacheModes cacheMode = CacheModes.Sliding)
        {
            T result = await GetAsync<T>(key);
            if (IsEquals(result, default))
            {
                using (await TypeLock<T>.Lock.LockAsync())
                {
                    result = Get<T>(key);
                    if (!IsEquals(result, default)) return result;
                    result = await data();
                    await SetAsync(key, result, expire, cacheMode);
                }
            }
            return result;
        }

        public T HashGetOrSet<T>(string key, string field, Func<T> data, DateTime expiry)
        {
            T result = HashGet<T>(key, field);
            if (IsEquals(result, default))
            {
                using (TypeLock<T>.Lock.Lock())
                {
                    result = HashGet<T>(key, field);
                    if (!IsEquals(result, default)) return result;
                    result = data();
                    HashSet(key, field, result, expiry);
                }
            }
            return result;
        }

        public async Task<T> HashGetOrSetAsync<T>(string key, string field, Func<Task<T>> data, DateTime expiry)
        {
            T result = await HashGetAsync<T>(key, field);
            if (IsEquals(result, default))
            {
                using (await TypeLock<T>.Lock.LockAsync())
                {
                    result = HashGet<T>(key, field);
                    if (!IsEquals(result, default)) return result;
                    result = await data();
                    await HashSetAsync(key, field, result, expiry);
                }
            }
            return result;
        }

        static NullableObject<T> Cast<T>(RedisValue redisValue)
        {
            if (redisValue.HasValue)
            {
                return JsonConvert.DeserializeObject<NullableObject<T>>(redisValue);
            }
            return new NullableObject<T>(default);
        }

        static RedisValue Cast<T>(T data)
        {
            return JsonConvert.SerializeObject(new NullableObject<T>(data), new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        private static bool IsEquals<T>(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }
    }
}
