using System.Collections.Generic;
using System.Threading.Tasks;

namespace H2h.RubberBand.Database.Crud
{
    public interface IConfigCrud
    {
        Task<(int? maxAgeSeconds, Dictionary<string, string> config)> GetConfigAsync(string serviceName, string environment);

        Task SaveConfig(string serviceName, string environment, int maxAgeSeconds, Dictionary<string, string> values = null);
    }
}