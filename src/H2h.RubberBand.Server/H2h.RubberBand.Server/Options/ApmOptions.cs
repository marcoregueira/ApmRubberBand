using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace H2h.RubberBand.Server.Options
{
    public class ApmOptions
    {
        public int DefaultCentralConfigurationMaxAge { get; set; } = 300;
        public bool AutoCreateCentralConfiguration { get; set; }
    }
}
