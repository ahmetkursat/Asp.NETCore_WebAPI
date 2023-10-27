using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilters
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);  //ctrl . yaparak methodu override edip sadece bu methodu seçtik method baslamadan eylem olsun diye

            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];

            //Dto

            var param = context.ActionArguments.SingleOrDefault(a => a.Value.ToString().Contains("Dto")).Value;

            if (param is null)
            {
                context.Result = new BadRequestObjectResult($"Object is Null " +
                    $"Controller {controller}" +
                    $"Action {action}");
                return;
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }


        }
    }
}
