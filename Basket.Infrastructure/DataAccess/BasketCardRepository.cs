using Basket.Domain.Entities;
using Basket.Domain.Localized;
using Basket.Infrastructure.DataAccess.Abstract;
using Basket.Infrastructure.MongoDb;

namespace Basket.Infrastructure.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class BasketCardRepository : MongoDbRepository<BasketCard>,IBasketCardRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mongoDbConnector"></param>
        /// <param name="localized"></param>
        public BasketCardRepository(IMongoDbConnector mongoDbConnector, IResponseLocalized localized)
            : base(mongoDbConnector, localized)
        {
        }
    }
}