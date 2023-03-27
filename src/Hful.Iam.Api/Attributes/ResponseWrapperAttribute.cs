using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hful.Iam.Api.Attributes
{
    public class ResponseWrapperAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var result = context.Result;
            if (result is ObjectResult objectResult)
            {
                context.Result = new ObjectResult(new
                {
                    Code = 0,
                    Data = objectResult.Value
                });
            }
            base.OnResultExecuting(context);
        }
    }
}
