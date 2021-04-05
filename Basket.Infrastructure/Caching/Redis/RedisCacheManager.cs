using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Basket.Domain.Extensions.Structures;
using Basket.Domain.SharedCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Basket.Infrastructure.Caching.Redis
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisCacheManager : ICacheService
    {
        private ConnectionMultiplexer _connection;
        private IDatabase _database;
        private readonly object _lock;
        private readonly RedisCacheSettings _redisCacheSettings;
        private readonly SemaphoreSlim _connectionLock = new SemaphoreSlim(1, 1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="redisCacheSettings"></param>
        public RedisCacheManager(IOptions<RedisCacheSettings> redisCacheSettings)
        {
            this._redisCacheSettings =
                redisCacheSettings.Value ?? throw new ArgumentNullException(nameof(RedisCacheSettings));
            _lock = new object();
            Connect();
        }

        private void Connect()
        {
            if (_database != null)
            {
                return;
            }

            _connectionLock.Wait();

            try
            {
                _connection = ConnectionMultiplexer.Connect(_redisCacheSettings.ConnectionString);
                _database = _connection.GetDatabase();
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        private async ValueTask ConnectAsync(CancellationToken cancellationToken = default)
        {
            if (_database != null)
            {
                return;
            }

            cancellationToken.ThrowIfCancellationRequested();

            await _connectionLock.WaitAsync(cancellationToken);
            try
            {
                _connection = await ConnectionMultiplexer.ConnectAsync(_redisCacheSettings.ConnectionString);
                _database = _connection.GetDatabase();
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        private string GetKey(string key)
        {
            return $"{_redisCacheSettings.InstanceName}-{key}";
        }

        private List<RedisKey> GetRemoveKeys(string pattern)
        {
            var endPoint = _connection.GetEndPoints().FirstOrDefault();
            return _connection.GetServer(endPoint).Keys(pattern: $"*{pattern}*").ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _connection?.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async ValueTask<T> GetAsync<T>(string key)
        {
            await ConnectAsync();
            var data = await _database.StringGetAsync(GetKey(key));
            return data.HasValue
                ? data.ToString().FromJson<T>()
                : default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Set(string key, object data, TimeSpan? expiry = null)
        {
            Connect();
            lock (_lock)
            {
                return _database.StringSet(GetKey(key), data.AsJson(), expiry, When.Always);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async ValueTask<bool> IsExistsAsync(string key)
        {
            await ConnectAsync();
            return await _database.KeyExistsAsync(GetKey(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async ValueTask<bool> RemoveAsync(string key)
        {
            await ConnectAsync();
            return await _database.KeyDeleteAsync(GetKey(key));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public async ValueTask RemoveByPatternAsync(string pattern)
        {
            await ConnectAsync();
            var keysToRemove = GetRemoveKeys(GetKey(pattern));
            if (keysToRemove.Any())
            {
                await _database.KeyDeleteAsync(keysToRemove.ToArray());
            }
        }
    }
}