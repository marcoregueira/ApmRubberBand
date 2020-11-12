using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace H2h.ElasticSearch.Model.IntakeModel
{
    public partial class SpanDto
    {
        public JToken Span { get; set; }

        public TransactionDtoInternal TransactionInfo { get; set; }

        public class TransactionDtoInternal
        {
            //"context":{},
            public decimal duration { get; set; }             //                373.395,

            public string id { get; set; }                    //  "d562fc8482b4b641",
            public bool sampled { get; set; }                 //        true,
            public string name { get; set; }                  //      " GetPendingOutputMessages",

                                                              // public span_count			  {get;set;}	  //
            public DateTime Timestamp { get; set; }           //                    1581631012472065,

            public string Transaction_id { get; set; }              //              "76d01eaf4700f78dbf06c68b035f9f27",
            public string Parent_id { get; set; }              //              "76d01eaf4700f78dbf06c68b035f9f27",
            public string Trace_id { get; set; }              //              "76d01eaf4700f78dbf06c68b035f9f27",
            public string Type { get; set; }                    //     "request"}
            public string remotehost { get; set; }
            public string database { get; set; }
            public ContextDto Context { get; set; }
        }

        public class ContextDto
        {
            public Dictionary<string, string> Tags { get; set; }
        }
    }
}