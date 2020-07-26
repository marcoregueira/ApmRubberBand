using System;

namespace H2h.ElasticSearch.Model.IntakeModel
{
    public class MetaDataDto
    {
        public MetadataInternal Metadata { get; set; }
        public class MetadataInternal
        {
            public Service Service { get; set; }
            public Framework Framework { get; set; }
            public AgentC Agent { get; set; }
            public System System { get; set; }
        }
    }
}
