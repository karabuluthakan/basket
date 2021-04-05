using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Basket.Application.AutoMapper;
using Basket.Application.PipelineBehaviors;
using Basket.Domain.IoC;
using Basket.Domain.Localized;
using Basket.Domain.Responses;
using Basket.Domain.Responses.Abstract;
using Basket.Domain.SharedCore;
using Basket.Infrastructure.Caching.Redis;
using Basket.Infrastructure.DataAccess;
using Basket.Infrastructure.DataAccess.Abstract;
using Basket.Infrastructure.MongoDb;
using Basket.Infrastructure.Providers;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Basket.Api.DependencyResolvers.ConfigureServices
{
    /// <summary>
    /// 
    /// </summary>
    public class InitializeServiceStartup : IConfigureServiceModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void Load(IServiceCollection services)
        {
            var applicationAssembly = AppDomain.CurrentDomain.Load("Basket.Application");
            var startupAssembly = typeof(Startup).Assembly;
            services.AddMediatR(applicationAssembly, startupAssembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            services.AddControllers(options => { options.EnableEndpointRouting = false; })
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                }).AddFluentValidation(opt =>
                {
                    opt.RegisterValidatorsFromAssemblyContaining<Startup>();
                    opt.ImplicitlyValidateChildProperties = true;
                }).AddMvcOptions(opt =>
                {
                    opt.ModelMetadataDetailsProviders.Clear();
                    opt.ModelValidatorProviders.Clear();
                });

            services.AddHttpContextAccessor();
            services.AddHttpClient();

            services.AddValidatorsFromAssemblies(new[] {applicationAssembly, startupAssembly});

            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);

            services.AddScoped<IPaginationUriService>(sp =>
            {
                var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                return new PaginationUriManager(httpContextAccessor);
            });

            services.AddSingleton<IMapperAdapter, AutoMapperAdapter>();
            services.AddScoped<ICacheService, RedisCacheManager>();
            services.AddTransient<IResponseLocalized, ResponseLocalized>();
            services.AddTransient<IProductStockProvider, ProductStockProvider>();

            services.AddScoped<IBasketCardRepository, BasketCardRepository>();

            #region DataAccess

            services.AddSingleton(typeof(MongoDbPersistence));

            const string mSc = "MongoSerializationConventions";
            var mongoConventionPack = new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention(),
                new NamedIdMemberConvention("Id", "id", "_id"),
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(false),
                new StringObjectIdIdGeneratorConvention(), // should be before LookupIdGeneratorConvention ,
            };
            ConventionRegistry.Register(mSc, mongoConventionPack, t => true);

            MongoDbPersistence.Configure();
            services.AddScoped<IMongoDbConnector, MongoDbConnector>();

            #endregion
        }
    }
}