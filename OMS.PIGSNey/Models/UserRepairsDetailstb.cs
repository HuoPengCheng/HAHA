
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMS.PIGSNey.Models
{
    //用户报修信息详情表
   public class UserRepairsDetailstb
    {
        [Key]
        public int UrdId { get; set; }
        //订单编号自增
        public string Ordernumber { get; set; }
        //商品类型
        public string Type { get; set; }
        //维修等级
        public int Degree { get; set; }

        //商品型号
        public string Marque { get; set; }
        //报修理由
        public string Reason { get; set; }
        //地址(省市区)
        public string Addrelss { get; set; }
        //详细地址
        public string DetailedAddress { get; set; }
        //获取当前时间
        public DateTime Date { get; set; }
        //用户Id(申请人Id)
        public int UId { get; set; }
        //状态
        public int State { get; set; }
    }
}
