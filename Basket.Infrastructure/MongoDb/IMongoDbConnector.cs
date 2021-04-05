using MongoDB.Driver;

namespace Basket.Infrastructure.MongoDb
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMongoDbConnector
    {
        /// <summary>
        /// 
        /// </summary>
        IMongoDatabase MongoDatabase { get; }
    }
}