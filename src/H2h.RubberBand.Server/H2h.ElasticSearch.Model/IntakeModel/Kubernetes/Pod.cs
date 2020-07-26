// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information
using H2h.ElasticSearch.Model.Serialization;
using Newtonsoft.Json;

namespace H2h.ElasticSearch.Model.IntakeModel.Kubernetes
{
    public class Pod
    {
        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        public string Name { get; set; }

        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        public string Uid { get; set; }
    }
}
