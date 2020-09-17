using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    public class FenYe<T> where T:class,new ()
    {
        public List<T> masd { get; set; }
        public int Zongyeshu { get; set; }
        public int Zongtiaoshu { get; set; }
        public int Dangqianye { get; set; }
    }
}
