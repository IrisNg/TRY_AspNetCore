using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace TRY_AspNetCore_API
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        }

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var item in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(item.GroupName, CreateVersionInfo(item));
            }
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            return new OpenApiInfo
            {
                Title = "Your Versioned API",
                Version = description.ApiVersion.ToString()
            };
        }
    }
}
