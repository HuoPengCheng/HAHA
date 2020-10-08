using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    public class ViewUserRole
    {
        [Key]
        public int UId { get; set; }
        //姓名
        public string UName { get; set; }
        //用户账号
        public string UAccount { get; set; }
        //用户密码
        public string UPwd { get; set; }
        //手机号
        public string UPhone { get; set; }
        //角色id
        public int RId { get; set; }
        //状态
        public int UState { get; set; }
        //角色名称
        public string RName { get; set; }
    }
}
