using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    public class Prompttb
    {
        [Key]
        public int PRId { get; set; }
        public string PromptContent { get; set; }
        public DateTime PromptTime { get; set; }
        public int UId { get; set; }
        public int UrdId { get; set; }
        public int PromptSet { get; set; }
    }
}
