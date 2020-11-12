using Newtonsoft.Json.Linq;
using System;

namespace H2h.ElasticSearch.Model.IntakeModel
{
    public class ErrorsetDto
    {
        public JToken Error { get; set; }

        public class ErrorDtoInternal
        {
            public string Id { get; set; }
            public string Culprit { get; set; }
            public string TraceId { get; set; }
            public string ParentId { get; set; }
            public string Transaction_Id { get; set; }
            public DateTime Timestamp { get; set; }
        }
    }
}