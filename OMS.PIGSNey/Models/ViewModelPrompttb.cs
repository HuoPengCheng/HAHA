using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    public class ViewModelPrompttb
    {
        [Key]
        public int PRId { get; set; }
        public string PromptContent { get; set; }
        public DateTime PromptTime { get; set; }
        public int UId { get; set; }
        public int UrdId { get; set; }
        //订单编号自增
        public string Ordernumber { get; set; }
        //姓名
        public string UName { get; set; }
    }
}
