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
        public async Task<ActionResult<IEnumerable<KeHuXianshi>>> KeHu(int uid)
        {
            var list = from u in db.UserInfotb
                       join r in db.UserRepairsDetailstb
                       on u.UId equals r.UId
                       select new KeHuXianshi()
                       {
                           UId = u.UId,
                           UName = u.UName,
                           UAccount = u.UAccount,
                           UPhone = u.UPhone,
                           Type = r.Type,
                           Reason = r.Reason
                       };
            list = list.Where(p => p.UId==uid);
            return await list.ToListAsync();

        }
        /// <summary>
        /// 维修员查看
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<KeHuXianshi>>> WeiXui(int uid)
        {
            var list = from u in db.UserInfotb
                       join r in db.UserRepairsDetailstb
                       on u.UId equals r.UId
                       join m in db.MaintenanceDetailstb
                       on r.UrdId equals m.URDId
                       select new KeHuXianshi()
                       {
                           UId=m.UId,
                           UName = u.UName,
                           UAccount = u.UAccount,
                           UPhone = u.UPhone,
                           Type = r.Type,
                           Reason = r.Reason,
                           State=r.State
                       };
            list = list.Where(p => p.UId==uid &&p.State==2||p.State==3||p.State==4 );
            return await list.ToListAsync();

        }
        /// <summary>
        /// 管理员查看
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<KeHuXianshi>>> GuanLi()
        {
            var list = from u in db.UserInfotb
                       join r in db.UserRepairsDetailstb
                       on u.UId equals r.UId
                       join m in db.MaintenanceDetailstb
                       on r.UrdId equals m.URDId
                       select new KeHuXianshi()
                       {
                           UId = m.UId,
                           UName = u.UName,
                           UAccount = u.UAccount,
                           UPhone = u.UPhone,
                           Type = r.Type,
                           Reason = r.Reason
                       };
            return await list.ToListAsync();

        }
        public async Task<ActionResult<IEnumerable<KeHuXianshi>>> ChaoJi(int uid)
        {
            var list = from u in db.UserInfotb
                       join r in db.UserRepairsDetailstb
                       on u.UId equals r.UId
                       select new KeHuXianshi()
                       {
                           UId = u.UId,
                           UName = u.UName,
                           UAccount = u.UAccount,
                           UPhone = u.UPhone,
                           Type = r.Type,
                           Reason = r.Reason
                       };
            list = list.Where(p => p.UId == uid);
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
        /// <summary>
        /// 维修员接单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>> ZhuangTai1(int id)
        {
            UserRepairsDetailstb b = db.UserRepairsDetailstb.Find(id);
            b.State = 2;
            return await db.SaveChangesAsync();
        }
        /// <summary>
        /// 维修员维修
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>> ZhuangTai2(int id)
        {
            UserRepairsDetailstb b = db.UserRepairsDetailstb.Find(id);
            b.State = 3;
            return await db.SaveChangesAsync();
        }
        /// <summary>
        /// 维修员维完成
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>> ZhuangTai3(int id)
        {
            UserRepairsDetailstb b = db.UserRepairsDetailstb.Find(id);
            b.State = 4;
            return await db.SaveChangesAsync();
        }
    }
}
