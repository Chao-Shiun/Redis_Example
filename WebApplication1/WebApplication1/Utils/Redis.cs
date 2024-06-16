using StackExchange.Redis;
using System;

namespace WebApplication1.Utils
{
    public class Redis
    {
        private readonly ConnectionMultiplexer redis;
        private readonly IDatabase db;
        private readonly TimeSpan defaultExpiry = TimeSpan.FromMinutes(10);

        public Redis(string configuration)
        {
            redis = ConnectionMultiplexer.Connect(configuration);
            db = redis.GetDatabase();
        }

        public void SetString(string key, string value, TimeSpan? expiry = null)
        {
            db.StringSet(key, value, expiry ?? defaultExpiry);
        }

        public string? GetString(string key)
        {
            return db.StringGet(key);
        }

        public void SetHash(string key, HashEntry[] hashEntries, TimeSpan? expiry = null)
        {
            db.HashSet(key, hashEntries);
            db.KeyExpire(key, expiry.HasValue ? expiry.Value : defaultExpiry);
        }

        public HashEntry[] GetHash(string key)
        {
            return db.HashGetAll(key);
        }
        
        public bool DeleteKey(string key)
        {
            return db.KeyDelete(key);
        }
    }
}