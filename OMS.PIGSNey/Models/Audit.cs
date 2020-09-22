using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    /// <summary>
    /// 审核领取表
    /// </summary>
    public class Audit
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int AUId { get; set; }
        /// <summary>
        /// 材料申请表Id
        /// </summary>
        public int AId { get; set; }
        /// <summary>
        /// 领取时间
        /// </summary>
        public DateTime  AuditDate { get; set; }
    }
}
