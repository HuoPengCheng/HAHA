
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMS.PIGSNey.Models
{
    //实名认证表
   public class RealNametb
    {
        [Key]
        public int RNId { get; set; }
        //认证人Id
        public int UId { get; set; }
        //身份证正面
        public string Prcture1 { get; set; }
        //身份证反面
        public string Prcture2 { get; set; }
    }
}
