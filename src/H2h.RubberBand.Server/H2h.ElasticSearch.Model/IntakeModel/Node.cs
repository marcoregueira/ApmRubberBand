﻿using H2h.ElasticSearch.Model.Serialization;
using Newtonsoft.Json;

namespace H2h.ElasticSearch.Model.IntakeModel
{
    public class Node
    {
        [JsonProperty("configured_name")]
        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        public string ConfiguredName { get; set; }
    }
}