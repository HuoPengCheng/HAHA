using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    /// <summary>
    /// 工具申请表
    /// </summary>
    public class AddTool
    {
        [Key]
        public int ATId { get; set; }

        public int TId { get; set; }

        public int UId { get; set; }

        public int AStatic { get; set; }

        public DateTime AppDate { get; set; }
    }
}
