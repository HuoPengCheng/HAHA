using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    public class AT
    {
        //ATId = at.ATId,
        //ToolName = t.ToolName,
        //UName = u.UName,
        //AppDate = at.AppDate
        public int ATId { get; set; }

        public string ToolName { get; set; }

        public string UName { get; set; }

        public DateTime AppDate { get; set; }
    }
}
