
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMS.PIGSNey.Models
{
    //投诉表
   public class Complaintb
    {
        [Key]
        public int CoId { get; set; }
        //工单号
        public string Ordernumber { get; set; }
        
        public int UId1 { get; set; }
        public int UId2 { get; set; }
        //评论
        public string Comment { get; set; }
        //照片
        public string Img { get; set; }
        //状态
        public int State { get; set; }
    }
}
