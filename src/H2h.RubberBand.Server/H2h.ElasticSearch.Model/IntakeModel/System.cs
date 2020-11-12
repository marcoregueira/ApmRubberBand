using H2h.ElasticSearch.Model.IntakeModel.Kubernetes;
using H2h.ElasticSearch.Model.Serialization;
using Newtonsoft.Json;

namespace H2h.ElasticSearch.Model.IntakeModel
{
    public class System
    {
        public KubernetesMetadata Kubernetes { get; set; }

        public Container Container { get; set; }

        [JsonProperty("detected_hostname")]
        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        public string DetectedHostName { get; set; }

        [JsonProperty("hostname")]
        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        public string HostName => DetectedHostName;
    }
}