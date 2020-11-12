using H2h.ElasticSearch.Model.Serialization;
using Newtonsoft.Json;

namespace H2h.ElasticSearch.Model.IntakeModel
{
    /// <summary>
    /// Name and version of the language runtime running this service
    /// </summary>
    public class Runtime
    {
        internal const string DotNetCoreName = ".NET Core";

        internal const string DotNetFullFrameworkName = ".NET Framework";

        internal const string MonoName = "Mono";

        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        public string Name { get; set; }

        [JsonConverter(typeof(TrimmedStringJsonConverter))]
        public string Version { get; set; }
    }
}