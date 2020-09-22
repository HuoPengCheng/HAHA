using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    [Table("wenjuan")]
    public class wenjuan
    {
        [Key]
        //问卷编号
        public int wjid { get; set; }
        //问卷名称
        public string mingcheng { get; set; }
        //发布时间
        public string shijian { get; set; }
    }
}
