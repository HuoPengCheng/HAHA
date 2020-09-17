
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMS.PIGSNey.Models
{
    public class PageAF
    {
        public List<AF> AF { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int totalPage { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int totalCount { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }
    }

    /// <summary>
    /// 材料申请表
    /// </summary>
    public class ApplyFortb
    {
        [Key]
        public int AId { get; set; }
        //材料名称
        public string MaterialName { get; set; }
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
