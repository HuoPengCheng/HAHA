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
        public FenYe<KeHuXianshi> KeHu(int uid = 0,string name="", int dangqianye=1 , int meiyetiaoshu=3 )
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
                           Reason = r.Reason,
                           UrdId=r.UrdId
                       };
            list = list.Where(p => p.UId == uid);
            if (!string.IsNullOrEmpty(name))
            {
                list = list.Where(p => p.Type.Contains(name));
            }
            if (dangqianye < 1)
            {
                dangqianye = 1;
            }
            var zongtiaoshu = list.Count();
            int page;
            if (zongtiaoshu % meiyetiaoshu == 0)
            {
                page = zongtiaoshu / meiyetiaoshu;
            }
            else
            {
                page = zongtiaoshu / meiyetiaoshu + 1;
            }
            if (dangqianye > page)
            {
                dangqianye = page;
            }

            if (zongtiaoshu == 0)
            {
                FenYe<KeHuXianshi> p = new FenYe<KeHuXianshi>();

                p.Zongtiaoshu = zongtiaoshu;
                p.Zongyeshu = page;
                p.Dangqianye = dangqianye;
                return p;
            }
            else
            {
                FenYe<KeHuXianshi> p = new FenYe<KeHuXianshi>();
                p.masd = list.Skip((dangqianye - 1) * meiyetiaoshu).Take(meiyetiaoshu).ToList();
                p.Zongtiaoshu = zongtiaoshu;
                p.Zongyeshu = page;
                p.Dangqianye = dangqianye;
                return p;
            }

        }
        /// <summary>
        /// 维修员查看
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public FenYe<KeHuXianshi> WeiXui(int uid, int dangqianye = 1, int meiyetiaoshu = 3)
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
                           Reason = r.Reason,
                           State = r.State
                       };
            list = list.Where(p => p.UId == uid && p.State == 2 || p.State == 3 || p.State == 4);
            if (dangqianye <= 1)
            {
                dangqianye = 1;
            }
            var zongtiaoshu = list.Count();
            int page;
            if (zongtiaoshu % meiyetiaoshu == 0)
            {
                page = zongtiaoshu / meiyetiaoshu;
            }
            else
            {
                page = zongtiaoshu / meiyetiaoshu + 1;
            }
            if (dangqianye >= page)
            {
                dangqianye = page;
            }

            FenYe<KeHuXianshi> k = new FenYe<KeHuXianshi>();
            k.masd = list.Skip((dangqianye - 1) * meiyetiaoshu).Take(meiyetiaoshu).ToList();
            k.Dangqianye = dangqianye;
            k.Zongtiaoshu = zongtiaoshu;
            k.Zongyeshu = page;
            return k;

        }
        /// <summary>
        /// 管理员查看
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public FenYe<KeHuXianshi> GuanLi(int dangqianye = 1, int meiyetiaoshu = 3)
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
                           Reason = r.Reason,
                           UrdId=r.UrdId
                       };
            if (dangqianye < 1)
            {
                dangqianye = 1;
            }
            var zongtiaoshu = list.Count();
            int page;
            if (zongtiaoshu % meiyetiaoshu == 0)
            {
                page = zongtiaoshu / meiyetiaoshu;
            }
            else
            {
                page = zongtiaoshu / meiyetiaoshu + 1;
            }
            if (dangqianye > page)
            {
                dangqianye = page;
            }

            FenYe<KeHuXianshi> k = new FenYe<KeHuXianshi>();
            k.masd = list.Skip((dangqianye - 1) * meiyetiaoshu).Take(meiyetiaoshu).ToList();
            k.Dangqianye = dangqianye;
            k.Zongtiaoshu = zongtiaoshu;
            k.Zongyeshu = page;
            return k;

        }
        /// <summary>
        /// 超级管理员
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<UserInfotb>>> ChaoJi(string acount)
        {
            return await db.UserInfotb.ToListAsync();
        }
        /// <summary>
        /// 冻结账户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>> ZhuangTai1(int id)
        {
            UserInfotb b = db.UserInfotb.Find(id);
            b.UState = 0;
            return await db.SaveChangesAsync();
        }
        /// <summary>
        /// 修改客户密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>> KeHuPwd(int id, string pwd)
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
        public async Task<ActionResult<int>> ZhuangTai2(int id, int id2)
        {
            UserRepairsDetailstb b = db.UserRepairsDetailstb.Find(id);
            b.State = 2;
            UserInfotb c = db.UserInfotb.Find(id2);
            c.UState = 2;
            return await db.SaveChangesAsync();
        }
        /// <summary>
        /// 维修员维修
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>> ZhuangTai3(int id)
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
        public async Task<ActionResult<int>> ZhuangTai4(int id)
        {
            UserRepairsDetailstb b = db.UserRepairsDetailstb.Find(id);
            b.State = 4;
            return await db.SaveChangesAsync();
        }
        /// <summary>
        /// 客户删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>>KDel(int id)
        {
            db.UserRepairsDetailstb.Remove(db.UserRepairsDetailstb.Find(id));
            return await db.SaveChangesAsync();
        }
    }
}
