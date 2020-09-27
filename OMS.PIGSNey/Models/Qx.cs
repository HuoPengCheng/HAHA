using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    public class Qx
    {
        [Key]
        public int PYId { get; set; }
        public int PYName { get; set; }
        public int PYFId { get; set; }
        public string Url { get; set; }

        public int KId { get; set; }



        public string WName { get; set; }
        public int WFId { get; set; }

        public int WRId { get; set; }
        public int wid { get; set; }
        public int rid { get; set; }

        public int GPId { get; set; }
        public int GPName { get; set; }
        public int GPFId { get; set; }

        public int CId { get; set; }
        public int PId { get; set; }
  

 
        public string PName { get; set; }
        public int Id { get; set; }
 

 
        //角色名称
        public string RName { get; set; }
    }
}
