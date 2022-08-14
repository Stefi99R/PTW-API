namespace PTW_API.Services
{
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.OpenApi.Models;
    using PTW_API.Contracts;
    using Swashbuckle.AspNetCore.Filters;
    using Swashbuckle.AspNetCore.SwaggerUI;
    using System.Reflection;
    using System.Collections.Generic;
    using System.Linq;

    public static class RegisterSwagger
    {
        public static IServiceCollection RegisterSwaggerDocumentation(this IServiceCollection service)
        {
            IAppSettings appSettings = service.BuildServiceProvider().GetRequiredService<IAppSettings>();
            ISwaggerSettings swaggerSettings = service.BuildServiceProvider().GetRequiredService<ISwaggerSettings>();
            IOAuthSettings oAuthSettings = service.BuildServiceProvider().GetRequiredService<IOAuthSettings>();

            service.AddSwaggerGen(x =>
            {
                x.SwaggerDoc(ApiVersions.ApiVersion1_0, new OpenApiInfo 
                { 
                    Title = appSettings.Version1_0.Name, 
                    Version = ApiVersions.ApiVersion1_0
                });

                x.DocInclusionPredicate((docName, apiDesc) =>
                {
                    ApiVersionModel? actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersionModel();

                    if (actionApiVersionModel == null)
                    {
                        // would mean this action is not version-ed and should be included everywhere
                        return true;
                    }

                    return actionApiVersionModel.ImplementedApiVersions.Any(y => y.ToString() == docName);
                });

                x.AddSecurityDefinition(swaggerSettings.AuthenticationTypeName, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = (SecuritySchemeType)swaggerSettings.AuthenticationType,
                    Description = swaggerSettings.Description,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = oAuthSettings.AuthorizeUrl,
                            Scopes = new Dictionary<string, string>
                            {
                                { "Key", swaggerSettings.Scope }
                            }
                        },
                    }
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = swaggerSettings.AuthenticationTypeName
                                }
                            },
                            new string[] { swaggerSettings.Scope }
                    }
                });

                x.EnableAnnotations();
                x.ExampleFilters();
                x.CustomSchemaIds(schemaSelector => schemaSelector.FullName);
            });

            service.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

            return service;
        }

        public static IApplicationBuilder UseSwagggerDocumentation(this IApplicationBuilder appBuilder)
        {
            IAppSettings? appSettings = appBuilder.ApplicationServices.GetService<IAppSettings>();
            IApiDocsSettings? apiDocsSettings = appBuilder.ApplicationServices.GetService<IApiDocsSettings>();
            ISwaggerSettings? swaggerSettings = appBuilder.ApplicationServices.GetService<ISwaggerSettings>();

            appBuilder.UseSwagger(options => options.RouteTemplate = apiDocsSettings?.RouteTemplate);
            appBuilder.UseSwaggerUI(x =>
            {
                x.RoutePrefix = apiDocsSettings?.RoutePrefix;
                x.DocExpansion(DocExpansion.None);
                x.SwaggerEndpoint(appSettings?.Version1_0.JsonEndpointUrl, appSettings?.Version1_0.Name);
                x.OAuthClientId(swaggerSettings?.ClientId);
                x.OAuthAppName(swaggerSettings?.ApplicationName);
            });

            return appBuilder;
        }
    }
}
