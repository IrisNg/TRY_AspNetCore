using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TRY_AspNetCore_API.ActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Early exit request pipeline without executing controller action
            // If request data fails ModelState validation
            if (context.ModelState.IsValid == false)
            {
                // Return 400 BadRequest
                context.Result = new BadRequestResult();
            }
        }
    }
}
