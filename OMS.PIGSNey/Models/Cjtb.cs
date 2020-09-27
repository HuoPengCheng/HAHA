using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    public class Cjtb
    {
        [Key]
        public int CId { get; set; }
        public int PId { get; set; }
        public int RId { get; set; }
    }
}
