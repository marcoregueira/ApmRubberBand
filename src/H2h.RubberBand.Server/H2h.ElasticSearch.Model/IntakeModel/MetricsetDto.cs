using System;
using System.Collections.Generic;

namespace H2h.ElasticSearch.Model.IntakeModel
{
    public class MetricsetDto
    {
        public MetricsetInternal Metricset { get; set; }

        public class MetricsetInternal
        {
            public Dictionary<string, Sample> Samples { get; set; }
            public DateTime Timestamp { get; set; }
        }
    }
}