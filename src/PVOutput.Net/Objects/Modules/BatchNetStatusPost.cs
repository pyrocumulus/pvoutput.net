using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVOutput.Net.Objects.Modules
{
    internal sealed class BatchNetStatusPost : IBatchNetStatusPost
    {
        public DateTime Timestamp { get; set; }
        public int? PowerExported { get; set; }
        public int? PowerImported { get; set; }
    }
}
