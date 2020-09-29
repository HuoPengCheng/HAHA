using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    public class GPermission
    {
        [Key]
        public int GPId { get; set; }
        public string PName { get; set; }
        public int Id { get; set; }
        public string Url { get; set; }
    }
}
