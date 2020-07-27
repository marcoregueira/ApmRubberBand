using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2h.RubberBand.Server.ETag
{
    // credit: https://stackoverflow.com/a/40455004/2982757
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ETagFilterAttribute : Attribute, IActionFilter
    {
        private readonly int[] _statusCodes;

        public ETagFilterAttribute(params int[] statusCodes)
        {
            _statusCodes = statusCodes;
            if (statusCodes.Length == 0) _statusCodes = new[] { 200 };
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var request = context.HttpContext.Request;

            if (request.Method == "GET"
                && context.Result is ObjectResult obj)
            {
                var content = JsonConvert.SerializeObject(context.Result);
                var etag = ETagGenerator.GetETag(context.HttpContext.Request.Path.ToString(), Encoding.UTF8.GetBytes(content));

                if (!etag.EndsWith("\""))
                    etag = "\"" + etag + "\"";

                var ifNoneMatch = request.Headers["If-None-Match"];
                if (ifNoneMatch == etag)
                {
                    context.Result = new StatusCodeResult(304);
                }
                context.HttpContext.Response.Headers.Add("ETag", etag);
            }
        }
    }
}
