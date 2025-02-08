using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IM_API.Utility
{
    public class AutoLogRequestAndResponse : TypeFilterAttribute
    {
        public AutoLogRequestAndResponse() : base(typeof(AutoLogActionFilter))
        {

        }

        private class AutoLogActionFilter : IActionFilter
        {
            static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AutoLogActionFilter));
            public AutoLogActionFilter()
            {
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                var LogKey = Guid.NewGuid();
                context.HttpContext.Response.Headers.Add("Audit-Trail-Key", LogKey.ToString());
                var reqBody = JsonConvert.SerializeObject(context.ActionArguments);
                var path = context.HttpContext.Request.Path;
                string reqString = $"\nService Call Url: {path}\n";
                reqString += $"Audit-Trail-Key: {LogKey.ToString()}\n";
                reqString += $"Service Request Message: {reqBody}";
                _log4net.Info(reqString);
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                if (context.Exception != null)
                {
                    throw context.Exception;
                }
                var result = context.Result;
                var LogKey = context.HttpContext.Response.Headers.Where(h => h.Key == "Audit-Trail-Key").FirstOrDefault().Value;
                if (result is ObjectResult json)
                {
                    var model = json.Value;
                    var status = json.StatusCode;
                    var resultStr = JsonConvert.SerializeObject(model);
                    string resString = $"\nAudit-Trail-Key: {LogKey.ToString()}\n";
                    resString += "Service Response Message: \n " + resultStr;
                    _log4net.Info(resString);
                }
                context.HttpContext.Response.Headers.Remove("Audit-Trail-Key");
            }
        }
    }
}
