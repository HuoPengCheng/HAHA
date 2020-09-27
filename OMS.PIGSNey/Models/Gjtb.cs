using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    public class Gjtb
    {
        [Key]
        public int GLId { get; set; }
        public int GId { get; set; }
        public int RId { get; set; }
    }
}
