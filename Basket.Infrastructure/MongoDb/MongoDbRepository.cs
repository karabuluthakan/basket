using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Basket.Domain.Entities.Abstract;
using Basket.Domain.Localized;
using Basket.Domain.Responses;
using Basket.Domain.Responses.Abstract;
using Basket.Domain.SharedCore;
using MongoDB.Driver;

namespace Basket.Infrastructure.MongoDb
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MongoDbRepository<T> : IRepository<T, string> where T : Entity, new()
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly string collectionName = typeof(T).Name.ToLowerInvariant();

        /// <summary>
        /// 
        /// </summary>
        protected readonly IMongoCollection<T> Collection;

        /// <summary>
        /// 
        /// </summary>
        protected readonly IResponseLocalized _localized;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mongoDbConnector"></param>
        /// <param name="localized"></param>
        protected MongoDbRepository(IMongoDbConnector mongoDbConnector, IResponseLocalized localized)
        {
            _localized = localized ?? throw new ArgumentNullException(nameof(localized));
            _mongoDatabase = mongoDbConnector.MongoDatabase;
            Collection = _mongoDatabase.GetCollection<T>(collectionName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Get(Expression<Func<T, bool>> predicate = null)
        {
            return Collection.AsQueryable().Where(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async ValueTask<T> GetAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            var data = await Collection.FindAsync(predicate, cancellationToken: cancellationToken);
            var response = await data.SingleOrDefaultAsync(cancellationToken);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async ValueTask<T> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var predicate = Builders<T>.Filter.Where(x => x.Id.Equals(id));
            var data = await Collection.FindAsync(predicate, cancellationToken: cancellationToken);
            var response = await data.FirstOrDefaultAsync(cancellationToken);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns> 
        public virtual async ValueTask<int> GetCountAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return (int) await Collection.CountDocumentsAsync(predicate, cancellationToken: cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async ValueTask<IResponse> AddAsync(T model, CancellationToken cancellationToken = default)
        {
            try
            {
                model.SetCreatedAt();
                await Collection.InsertOneAsync(model, new InsertOneOptions
                {
                    BypassDocumentValidation = false
                }, cancellationToken);
                return new SuccessResponse(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                return new ErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async ValueTask<IResponse> UpdateAsync(string id, T model,
            CancellationToken cancellationToken = default)
        {
            var predicate = Builders<T>.Filter.Eq(x => x.Id, id);
            var result = await Collection.FindOneAndReplaceAsync(predicate, model, new FindOneAndReplaceOptions<T>
            {
                IsUpsert = false,
                BypassDocumentValidation = true
            }, cancellationToken);

            if (result is null)
            {
                return new ErrorResponse(HttpStatusCode.NotFound, _localized.GetString("NotFound"));
            }

            return new SuccessDataResponse<T>(result, HttpStatusCode.NoContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async ValueTask<IResponse> UpdateAsync(Expression<Func<T, bool>> predicate, T model,
            CancellationToken cancellationToken = default)
        {
            var result = await Collection.FindOneAndReplaceAsync(predicate, model, new FindOneAndReplaceOptions<T>
            {
                IsUpsert = false,
                BypassDocumentValidation = true
            }, cancellationToken);
            if (result is null)
            {
                return new ErrorResponse(HttpStatusCode.NotFound, _localized.GetString("NotFound"));
            }

            return new SuccessDataResponse<T>(result, HttpStatusCode.NoContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async ValueTask<IResponse> DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            var predicate = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
            var result = await Collection.DeleteOneAsync(predicate, cancellationToken);
            if (result.IsAcknowledged)
            {
                return new SuccessResponse(HttpStatusCode.NoContent, entity.Id);
            }

            return new ErrorResponse(HttpStatusCode.NotFound, _localized.GetString("NotFound"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async ValueTask<IResponse> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var predicate = Builders<T>.Filter.Eq(x => x.Id, id);
            var result = await Collection.DeleteOneAsync(predicate, cancellationToken);
            if (result.IsAcknowledged)
            {
                return new SuccessResponse(HttpStatusCode.NoContent, id);
            }

            return new ErrorResponse(HttpStatusCode.NotFound, _localized.GetString("NotFound"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async ValueTask<IResponse> DeleteAsync(Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            var result = await Collection.DeleteOneAsync(predicate, cancellationToken);
            if (result.IsAcknowledged)
            {
                return new SuccessResponse(HttpStatusCode.NoContent);
            }

            return new ErrorResponse(HttpStatusCode.NotFound, _localized.GetString("NotFound"));
        }
    }
}