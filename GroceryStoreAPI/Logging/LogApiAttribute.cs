using System;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;

namespace GroceryStoreAPI.Logging
{
    public class LogApiAttribute : ActionFilterAttribute
    {
        private readonly ILog _logger;

        public LogApiAttribute()
        {
        }

        public LogApiAttribute(ILog logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _logger.Information(string.Format("Action Method {0} executing at {1}", actionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()));
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _logger.Information(string.Format("Action Method {0} executed at {1}", actionExecutedContext.ActionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()));
        }
    }
}