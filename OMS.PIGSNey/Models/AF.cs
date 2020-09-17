using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    public class AF
    {
        //MaterialName = 

        //MaterialAmount = 
        //t,
        //UName = u.UName,
        //AppDate = a.AppDate,
        //AStatic = a.AStatic
        public int AId { get; set; }

        public string MaterialName { get; set; }

        public int MaterialAmount { get; set; }

        public string UName { get; set; }

        public DateTime AppDate { get; set; }

        public int AStatic { get; set; }
    }
}
