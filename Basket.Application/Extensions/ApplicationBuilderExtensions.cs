using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using Basket.Domain.Extensions.Structures;
using Basket.Domain.Localized;
using Basket.Domain.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Basket.Application.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns> 
        public static IApplicationBuilder UseFluentValidationExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(x =>
            {
                x.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = feature.Error;

                    if (!(exception is ValidationException validationException))
                    {
                        throw exception;
                    }

                    var errors = validationException.Errors.Select(s => new
                    {
                        s.PropertyName,
                        s.ErrorMessage
                    }).AsJson();

                    var response = new ErrorResponse(HttpStatusCode.BadRequest, errors).AsJson();
                    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(response, Encoding.UTF8);
                });
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestLocalized(this IApplicationBuilder app)
        {
            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo(LocalizedConstants.EnUS),
                new CultureInfo(LocalizedConstants.EnGB),
                new CultureInfo(LocalizedConstants.DefaultCultureInfo)
            };

            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(LocalizedConstants.DefaultCultureInfo),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            return app.UseRequestLocalization(localizationOptions);
        }
    }
}