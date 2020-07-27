using H2h.ElasticSearch.Model.Serialization;
using Newtonsoft.Json;
using System;

namespace H2h.ElasticSearch.Model.IntakeModel
{
    public class User
    {
        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        public string Email { get; set; }

        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        public string Id { get; set; }

        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        [JsonProperty("username")]
        public string UserName { get; set; }
    }
}
