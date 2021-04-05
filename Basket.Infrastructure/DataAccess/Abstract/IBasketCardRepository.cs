using Basket.Domain.Entities;
using Basket.Domain.SharedCore;

namespace Basket.Infrastructure.DataAccess.Abstract
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBasketCardRepository : IRepository<BasketCard, string>
    {
    }
}