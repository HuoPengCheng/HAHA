 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    /// <summary>
    /// 工具领取时间
    /// </summary>
    public class AuditTool
    {
        [Key]
        public int ADTId { get; set; }

        public int ATId { get; set; }

        public DateTime AuditToolDate { get; set; }
    }
}
