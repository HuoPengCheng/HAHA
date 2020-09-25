using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    public class TS
    {
        //TId = t.TId,
        // ToolName = t.ToolName,
        // Img = t.Img
        public int TId { get; set; }

        public string ToolName { get; set; }
        public string TSpecification { get; set; }

        public string Img { get; set; }
    }
}
