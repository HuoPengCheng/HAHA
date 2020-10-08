using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    [Table("xuanxiang")]
    public class xuanxiang
    {
        [Key]
        //选项编号
        public int xxid { get; set; }
        //选项内容
        public string xuanxiangneirong { get; set; }
        //票数
        public int piaoshu { get; set; }
        //题目id
        public int tm_id { get; set; }
    }
}
