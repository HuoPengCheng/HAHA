
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMS.PIGSNey.Models
{
    /// <summary>
    /// 工具表
    /// </summary>
   public class Tooltb
    {
        [Key]
        //主键
        public int TId { get; set; }
        //工具名称
        public string ToolName { get; set; }
        //工具规格
        public string TSpecification { get; set; }
        //工具类别
        public string TCategory { get; set; }
        //图片
        public string Img { get; set; }
    }
}
