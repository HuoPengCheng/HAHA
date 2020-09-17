using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMS.PIGSNey.Models;

namespace OMS.PIGSNey.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CenterController : ControllerBase
    {
        //霍大帅哥
        public OMSContext db;
        public CenterController(OMSContext db) { this.db = db; }
        /// <summary>
        /// 查看客户中心
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<KeHuXianshi>>> KeHu(string acount)
        {
            var list = from u in db.UserInfotb
                       join r in db.UserRepairsDetailstb
                       on u.UId equals r.UId
                       select new KeHuXianshi()
                       {
                           UName = u.UName,
                           UAccount = u.UAccount,
                           UPhone = u.UPhone,
                           Type = r.Type,
                           Reason = r.Reason
                       };
            list = list.Where(p => p.UAccount.Contains(acount));
            return await list.ToListAsync();

        }
        /// <summary>
        /// 修改客户密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>>KeHuPwd(int id,string pwd)
        {
            UserInfotb b = db.UserInfotb.Find(id);
            b.UPwd = pwd;
            return await db.SaveChangesAsync();
        }
    }
}
