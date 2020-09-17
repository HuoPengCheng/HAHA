
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMS.PIGSNey.Models
{
    

    /// <summary>
    /// 材料申请表
    /// </summary>
    public class ApplyFortb
    {
        [Key]
        public int AId { get; set; }
        //材料名称/工具名称
        public int MAId { get; set; }
        public int UId { get; set; }
        //申请数量
        public int MaterialAmount { get; set; }

        /// <summary>
        /// 状态（0：未审批 1:已审批）
        /// </summary>
        public int AStatic { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
       public DateTime AppDate { get; set; }
    }
}
