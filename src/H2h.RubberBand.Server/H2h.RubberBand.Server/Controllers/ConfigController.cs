using H2h.RubberBand.Database.Crud;
using H2h.RubberBand.Server.ETag;
using H2h.RubberBand.Server.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H2h.RubberBand.Server.Controllers
{
    public class ConfigController : Controller
    {
        readonly ApmOptions apmOptions;
        readonly IConfigCrud crud;

        public ConfigController(IConfigCrud crud, IOptions<ApmOptions> apmOptions)
        {
            this.crud = crud;
            this.apmOptions = apmOptions.Value;
        }

        [HttpGet]
        [ETagFilter(200)]
        [Route("/config/v1/agents")]
        public async Task<IActionResult> ConfigAsync(
            [FromQuery(Name = "service.name")] string serviceName,
            [FromQuery(Name = "service.environment")] string serviceEnvironment
            )
        {
            if (string.IsNullOrWhiteSpace(serviceName) || string.IsNullOrWhiteSpace(serviceEnvironment))
                return BadRequest();

            var options = await crud.GetConfigAsync(serviceName, serviceEnvironment);
            if (options.config != null)
            {
                Response.Headers.Remove("Cache-Control");
                Response.Headers.Add("Cache-Control", $"public,max-age={options.maxAgeSeconds}");
                return Ok(options.config);
            }

            Dictionary<string, string> resultTemplate = GetOptionsTemplate();
            await SaveDefaultOptionsIfRequired(serviceName, serviceEnvironment, options, resultTemplate);

            Response.Headers.Remove("Cache-Control");
            Response.Headers.Add("Cache-Control", $"public,max-age={apmOptions.DefaultCentralConfigurationMaxAge}");

            return Ok(resultTemplate);
        }

        private async Task SaveDefaultOptionsIfRequired(string serviceName, string serviceEnvironment, (int? maxAgeSeconds, Dictionary<string, string> config) options, Dictionary<string, string> result)
        {
            if (options.config == null && apmOptions.AutoCreateCentralConfiguration)
                await crud.SaveConfig(serviceName, serviceEnvironment, apmOptions.DefaultCentralConfigurationMaxAge, result);
        }

        private static Dictionary<string, string> GetOptionsTemplate()
        {
            return new Dictionary<string, string>()
                {
                    {"_capture_body"                             ,""},
                    {"_capture_body_content_types"               ,""},
                    {"_transaction_max_spans"                    ,""},
                    {"_transaction_sample_rate"                  ,""},
                    {"_capture_headers"                          ,""},
                    {"_log_level"                                ,""},
                    {"_span_frames_min_duration"                 ,""},
                    {"_stack_trace_limit"                        ,""}
                };
        }
    }
}
