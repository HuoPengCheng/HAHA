
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMS.PIGSNey.Models
{
    //问卷评分表
    public class Questiontb 
    {
        [Key]
        public int QId { get; set; }
        //问题1
        public string Question1 { get; set; }
        //问题2
        public string Question2 { get; set; }
        //问题3
        public string Question3 { get; set; }
        //问题4
        public string Question4 { get; set; }
        //分数
        public int QNumber { get; set; }
    }
}

