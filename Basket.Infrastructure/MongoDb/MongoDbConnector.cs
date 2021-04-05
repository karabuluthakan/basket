using System;
using System.Text;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Basket.Infrastructure.MongoDb
{
    /// <summary>
    /// 
    /// </summary>
    public class MongoDbConnector : IMongoDbConnector
    {
        /// <summary>
        /// 
        /// </summary>
        public IMongoDatabase MongoDatabase { get; private set; }

        private readonly MongoDbSettings _mongoDbSettings;
        private readonly ObjectPool<StringBuilder> _stringBuilderPool;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mongoDbSettings"></param>
        /// <param name="stringBuilderPool"></param>
        public MongoDbConnector(IOptions<MongoDbSettings> mongoDbSettings, ObjectPool<StringBuilder> stringBuilderPool)
        {
            _mongoDbSettings = mongoDbSettings.Value ?? throw new ArgumentNullException(nameof(mongoDbSettings.Value));
            _stringBuilderPool = stringBuilderPool ?? throw new ArgumentNullException(nameof(stringBuilderPool));
            CreateMongoClient();
        }

        private void CreateMongoClient()
        {
            if (this.MongoDatabase is null)
            {
                var connectionString = GetConnectionString();
                var defaultDb = GetDefaultDb();
                var client = new MongoClient(connectionString);
                this.MongoDatabase = client.GetDatabase(defaultDb);
            }
        }

        private string GetConnectionString()
        {
            var stringBuilder = _stringBuilderPool.Get();
            stringBuilder.Append("mongodb://");
            stringBuilder.Append(_mongoDbSettings.ConnectionUrl);
            stringBuilder.Append(":27017/");
            stringBuilder.Append(_mongoDbSettings.ServiceName.Trim().ToLowerInvariant());
            stringBuilder.Append("-");
            stringBuilder.Append(_mongoDbSettings.Environment.Trim().ToLowerInvariant());
            stringBuilder.Append("?retryWrites=true");
            var result = stringBuilder.ToString();
            _stringBuilderPool.Return(stringBuilder);
            return result;
        }

        private string GetDefaultDb()
        {
            var stringBuilder = _stringBuilderPool.Get();
            stringBuilder.Append(_mongoDbSettings.ServiceName.Trim().ToLowerInvariant());
            stringBuilder.Append("-");
            stringBuilder.Append(_mongoDbSettings.Environment.Trim().ToLowerInvariant());
            var result = stringBuilder.ToString();
            _stringBuilderPool.Return(stringBuilder);
            return result;
        }
    }
}