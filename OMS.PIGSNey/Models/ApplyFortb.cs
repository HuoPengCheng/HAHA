
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMS.PIGSNey.Models
{
    //材料申请表
   public class ApplyFortb
    {
        [Key]
        public int AId { get; set; }
        //材料名称
        public string MaterialName { get; set; }
        public int UId { get; set; }
        //申请数量
        public int MaterialAmount { get; set; }
    }
}
