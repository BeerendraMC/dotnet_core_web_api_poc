using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace EmployeeManagement.Utility
{
    public class HeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            if (operation.Tags[0]?.CompareTo("Employee") == 0)
            {
                //operation.Parameters.Add(new NonBodyParameter
                //{
                //    Name = "Authorization",
                //    In = "header",
                //    Type = "string",
                //    Required = true
                //});
            }

        }

    }
}
