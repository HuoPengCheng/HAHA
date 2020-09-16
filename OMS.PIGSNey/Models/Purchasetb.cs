
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMS.PIGSNey.Models
{
    //采购表
   public class Purchasetb
    {
        [Key]
        public int PId { get; set; }
        //材料名称
        public string MAterialName { get; set; }
        //材料类别
        public string Category { get; set; }
        //采购数量
        public int PAmount { get; set; }
    }
}
