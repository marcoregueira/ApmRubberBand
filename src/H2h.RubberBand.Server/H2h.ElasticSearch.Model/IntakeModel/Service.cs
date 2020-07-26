using H2h.ElasticSearch.Model.Serialization;
using Newtonsoft.Json;
using System;

namespace H2h.ElasticSearch.Model.IntakeModel
{
    public class Service
    {
        private Service() { }

        public AgentC Agent { get; set; }

        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        public string Environment { get; set; }

        public Framework Framework { get; set; }
        public Language Language { get; set; }

        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        public string Name { get; set; }

        public Runtime Runtime { get; set; }

        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        public string Version { get; set; }

        public Node Node { get; set; }


    }
}
