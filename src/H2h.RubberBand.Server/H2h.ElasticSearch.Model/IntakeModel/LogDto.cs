using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace H2h.ElasticSearch.Model.IntakeModel
{
    public class LogDto
    {
        public JToken LogInfo { get; set; }

        public LogDtoInternal Log { get; set; }

        public class LogDtoInternal
        {
            public string Id { get; set; }
            public string Culprit { get; set; }
            public string TraceId { get; set; }
            public string ParentId { get; set; }
            public string Transaction_Id { get; set; }
            public DateTime Timestamp { get; set; }
            public string Level { get; set; }
            public string Message { get; set; }
            public Dictionary<string, string> LogInfo { get; set; }
        }
    }
}