using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    /// <summary>
    /// 类型表（材料/工具）
    /// </summary>
    public class Category
    {

        [Key]
        public int CId { get; set; }

        public string CName { get; set; }
    }
}
