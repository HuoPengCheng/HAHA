using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    public class Wxtb
    {
        [Key]
        public int WRId { get; set; }
        public int wid { get; set; }
        public int rid { get; set; }
    }
}
