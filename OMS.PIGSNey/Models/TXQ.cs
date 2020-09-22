using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    public class TXQ
    {

        //TId = t.TId,
        //  UName = u.UName,
        //  Marque = ud.Marque,
        //  Type = ud.Type,
        //  State = ud.State,
        //  UPhone = u.UPhone,
        //  AuditToolDate = a.AuditToolDate,
        //  ToolName = t.ToolName

        public int TId { get; set; }
       

        public string UName { get; set; }

        public string Marque { get; set; }

        public string Type { get; set; }

        public int State { get; set; }

        public string UPhone { get; set; }

        public DateTime AuditToolDate { get; set; }

        public string ToolName { get; set; }
    }
}
