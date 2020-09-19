
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMS.PIGSNey.Models
{
    /// <summary>
    /// 材料表
    /// </summary>
    public class Materialstb
    {
        [Key]
        public int MAId { get; set; }
        //材料名称
        public string MaterialName { get; set; }
        //规格
        public string MSpecification { get; set; }
        //材料类别
        public int CategoryId { get; set; }
        //数量
        public int MAmount { get; set; }
        //图片
        public string MImg { get; set; }
    }
}
