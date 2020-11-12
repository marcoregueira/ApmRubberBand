using H2h.ElasticSearch.Model.Serialization;
using Newtonsoft.Json;

namespace H2h.ElasticSearch.Model.IntakeModel
{
    public class Framework
    {
        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        public string Name { get; set; }

        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        public string Version { get; set; }
    }
}