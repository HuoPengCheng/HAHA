using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMS.PIGSNey.Models
{
    public class Jurisdiction
    {
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


        //角色名称
        public string RName { get; set; }

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

        //状态
        public int UState { get; set; }
    }
}
