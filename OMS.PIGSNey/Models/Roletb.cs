
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMS.PIGSNey.Models
{
    //角色表
   public class Roletb
    {
        [Key]
        public int RId { get; set; }
        //角色名称
        public string RName { get; set; }
    }
}
