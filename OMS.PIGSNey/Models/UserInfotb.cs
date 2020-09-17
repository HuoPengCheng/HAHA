using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace OMS.PIGSNey.Models
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class UserInfotb
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

    }
}
