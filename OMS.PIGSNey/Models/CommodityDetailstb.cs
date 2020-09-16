
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMS.PIGSNey.Models
{
    //商品详情表  信息二级联动
   public class CommodityDetailstb
    {
        [Key]
        public int CId { get; set; }
        //商品名称
        public string CName { get; set; }
        //父级Id
        public int CPId { get; set; }
    }
}
