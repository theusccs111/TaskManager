using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Task.Manager.Domain.Extensions;

namespace TaskManager.Web.Filters
{
    public class RequestValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.ModelState.IsValid)
            {
                throw new Exception(filterContext.ModelState.ToExceptionMessage());
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}
