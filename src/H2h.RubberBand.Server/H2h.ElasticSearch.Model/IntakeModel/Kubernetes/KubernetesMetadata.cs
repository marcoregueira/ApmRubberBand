// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information
using H2h.ElasticSearch.Model.Elastic.Apm.Helpers;
using H2h.ElasticSearch.Model.Serialization;
using Newtonsoft.Json;

namespace H2h.ElasticSearch.Model.IntakeModel.Kubernetes
{
    public class KubernetesMetadata
    {
        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        public string Namespace { get; set; }

        public Node Node { get; set; }

        public Pod Pod { get; set; }

        public override string ToString() =>
            new ToStringBuilder(nameof(KubernetesMetadata)) { { nameof(Namespace), Namespace }, { nameof(Node), Node }, { nameof(Pod), Pod } }
                .ToString();
    }
}
