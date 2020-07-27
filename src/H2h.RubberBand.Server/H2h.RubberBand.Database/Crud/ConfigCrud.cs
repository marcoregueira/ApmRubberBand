using H2h.RubberBand.Database.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H2h.RubberBand.Database.Crud
{
    public class ConfigCrud : IConfigCrud
    {
        private readonly ILogger<ConfigCrud> logger;
        private readonly BaseContext context;

        public ConfigCrud(BaseContext context, ILogger<ConfigCrud> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<(int? maxAgeSeconds, Dictionary<string, string> config)> GetConfigAsync(string serviceName, string environment)
        {
            // SEE CentralConfigReader.UpdateConfigurationValues for an up to date list of allowed values
            // CaptureBody                     
            // CaptureBodyContentTypes         
            // TransactionMaxSpans             
            // TransactionSampleRate           
            // CaptureHeaders                  
            // LogLevel                        
            // SpanFramesMinDurationInMilliseconds
            // StackTraceLimit                    

            var config = await context.ClientConfig
                .Where(x => x.ServiceName == serviceName || x.ServiceEnvironment == environment)
                .FirstOrDefaultAsync();

            return (config?.MaxAgeSeconds, config?.Options);
        }

        public async Task SaveConfig(
            string serviceName,
            string environment,
            int maxAgeSeconds,
            Dictionary<string, string> values = null)
        {
            // SEE: CentralConfigReader.UpdateConfigurationValues for an up to date list of allowed values
            // CaptureBody                     
            // CaptureBodyContentTypes         
            // TransactionMaxSpans             
            // TransactionSampleRate           
            // CaptureHeaders                  
            // LogLevel                        
            // SpanFramesMinDurationInMilliseconds
            // StackTraceLimit                    

            var config = await context.ClientConfig
                .Where(x => x.ServiceName == serviceName || x.ServiceEnvironment == environment)
                .FirstOrDefaultAsync()

                ?? new Entities.ClientConfigEntity()
                {
                    MaxAgeSeconds = maxAgeSeconds,
                    ServiceName = serviceName,
                    ServiceEnvironment = environment
                };

            values = values ?? new Dictionary<string, string>();

            config.OptionValues = JsonConvert.SerializeObject(values);

            if (config.LineId == 0)
            {
                context.Add(config);
                context.SaveChanges();
            }
        }
    }
}