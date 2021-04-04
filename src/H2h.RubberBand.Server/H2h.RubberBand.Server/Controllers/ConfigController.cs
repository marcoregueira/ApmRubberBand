using H2h.RubberBand.Database.Crud;
using H2h.RubberBand.Server.ETag;
using H2h.RubberBand.Server.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H2h.RubberBand.Server.Controllers
{
    public class ConfigController : Controller
    {
        private readonly ApmOptions apmOptions;
        private readonly IConfigCrud configRepository;

        public ConfigController(IConfigCrud crud, IOptions<ApmOptions> apmOptions)
        {
            this.configRepository = crud;
            this.apmOptions = apmOptions.Value;
        }

        [HttpGet]
        [ETagFilter(200)]
        [Route("/config/v1/agents")]
        public async Task<IActionResult> ConfigAsync(
            [FromQuery(Name = "service.name")] string serviceName,
            [FromQuery(Name = "service.environment")] string serviceEnvironment)

        {
            if (string.IsNullOrWhiteSpace(serviceName) || string.IsNullOrWhiteSpace(serviceEnvironment))
                return BadRequest();

            (var responseExpiration, var serviceConfig) = await configRepository.GetConfigAsync(serviceName, serviceEnvironment);

            if (serviceConfig == null)
            {
                serviceConfig = await GenerateNewConfiguration(serviceName, serviceEnvironment, serviceConfig);
                responseExpiration = apmOptions.DefaultCentralConfigurationMaxAge;
            }

            RemoveNullEntries(serviceConfig);

            Response.Headers.Remove("Cache-Control");
            Response.Headers.Add("Cache-Control", $"public,max-age={responseExpiration}");
            return Ok(serviceConfig);
        }

        private static void RemoveNullEntries(Dictionary<string, string> config)
        {
            foreach (var pair in config.ToList().Where(x => x.Value == null))
                config.Remove(pair.Key);
        }

        private async Task<Dictionary<string, string>> GenerateNewConfiguration(string serviceName, string serviceEnvironment, Dictionary<string, string> config)
        {
            config = GetOptionsTemplate();
            if (apmOptions.AutoCreateCentralConfiguration)
                await configRepository.SaveConfig(serviceName, serviceEnvironment, apmOptions.DefaultCentralConfigurationMaxAge, config);
            return config;
        }

        private static Dictionary<string, string> GetOptionsTemplate()
        {
            return new Dictionary<string, string>()
                {
                    {"capture_body"                             ,null},
                    {"capture_body_content_types"               ,null},
                    {"capture_headers"                          ,null},

                    {"log_level"                                ,null},
                    {"recording"                                ,null},

                    {"sanitize_field_names"                     ,null},
                    {"span_frames_min_duration"                 ,null},
                    {"stack_trace_limit"                        ,null},

                    {"transaction_ignore_urls"                  ,null},
                    {"transaction_max_spans"                    ,null},
                    {"transaction_sample_rate"                  ,null},
                };
        }
    }
}