using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace PocketApi.Config
{
    public class EnumDescriptionFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                schema.Description = "Value possibles :\n";
                foreach (var value in Enum.GetValues(context.Type))
                {
                    var fieldInfo = context.Type.GetField(value.ToString() ?? "");
                    var descriptionAttribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
                    schema.Description += $"- {value} : {descriptionAttribute?.Description ?? "No description"}\n";
                }
            }
        }
    }
}
