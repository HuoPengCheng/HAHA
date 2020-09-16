
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMS.PIGSNey.Models
{
    //权限分配表
   public class Juristb
    {
        [Key]
        public int JId { get; set; }
        //角色id
        public int RId { get; set; }
        //添加的权限
        public int JAdd { get; set; }
        //删除的权限
        public int JDel { get; set; }
        //显示的权限
        public int JShow { get; set; }
        //修改的权限
        public int JUpt { get; set; }
    }
}
