using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    [Table("timu")]
    public class timu
    {
        [Key]
        //题目编号
        public int tmid { get; set; }
        //题目标题
        public string biaoti { get; set; }
        //问卷id
        public string wj_id { get; set; }
    }
}
