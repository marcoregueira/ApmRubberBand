using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace H2h.ElasticSearch.Model.IntakeModel
{
    public class TransactionDto
    {
        public JToken Transaction { get; set; }

        public TransactionDtoInternal TransactionInfo { get; set; }

        public class TransactionDtoInternal
        {
            //"context":{},
            public decimal duration { get; set; }

            public string id { get; set; }
            public bool sampled { get; set; }
            public string name { get; set; }

            public DateTime Timestamp { get; set; }
            public string Trace_id { get; set; }
            public string Type { get; set; }
            public string Result { get; set; }
            public ContextDto Context { get; set; }
        }

        public class ContextDto
        {
            public User User { get; set; }

            public Dictionary<string, string> Tags { get; set; }
        }
    }
}