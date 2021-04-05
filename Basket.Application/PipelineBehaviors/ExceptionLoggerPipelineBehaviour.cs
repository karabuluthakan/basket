using System;
using System.Threading;
using System.Threading.Tasks;
using Basket.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Basket.Application.PipelineBehaviors
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class ExceptionLoggerPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ExceptionLoggerPipelineBehaviour<TRequest, TResponse>> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ExceptionLoggerPipelineBehaviour(ILogger<ExceptionLoggerPipelineBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (ValidationException exception)
            {
                _logger.LogWarning(exception, exception.Message);
                throw;
            }
            catch (DomainException exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, exception.Message);
                throw;
            }
        }
    }
}