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
                           UrdId=r.UrdId,
                           UState=u.UState,
                           State=r.State,
                           Marque=r.Marque
                       };
            list = list.Where(p => p.UId == 12);
            if (!string.IsNullOrEmpty(name))
            {
                list = list.Where(p => p.Marque.Contains(name));
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
        /// 维修员查看接单
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public FenYe<KeHuXianshi> WeiXui(int uid, int dangqianye = 1, int meiyetiaoshu = 3)
        {
            var list = from  r in db.UserRepairsDetailstb
                       select new KeHuXianshi()
                       {
                           UId=r.UId,
                           Type = r.Type,
                           Reason = r.Reason,
                           State = r.State,
                           UrdId=r.UrdId,
                           Marque=r.Marque,
                       };
            list = list.Where(p =>p.State==1);
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
        /// 个人
        /// </summary>
        /// <param name="dangqianye"></param>
        /// <param name="meiyetiaoshu"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public FenYe<KeHuXianshi> GeRenWx( int dangqianye = 1, int meiyetiaoshu = 3,string name="", int uid=0)
        {
            var list = from a in db.UserRepairsDetailstb
                       join b in db.UserInfotb on a.UId equals b.UId
                       join c in db.MaintenanceDetailstb on a.UrdId equals c.URDId
                       select new KeHuXianshi()
                       {
                           UName=b.UName,
                           UPhone=b.UPhone,
                           UId = c.UId,
                           Type = a.Type,
                           Reason = a.Reason,
                           State = a.State,
                           UrdId = a.UrdId,
                           Marque = a.Marque,


                       };
            list = list.Where(p => p.UId == uid&& p.State>1);
            if (!string.IsNullOrEmpty(name))
            {
                list = list.Where(p => p.Marque.Contains(name));
            }
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
        /// 管理员查看
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public FenYe<KeHuXianshi> GuanLi(int dangqianye = 1, int meiyetiaoshu = 3)
        {
            var list = from u in db.UserInfotb
                       join r in db.UserRepairsDetailstb on u.UId equals r.UId
                       join m in db.MaintenanceDetailstb on r.UrdId equals m.URDId
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
        public FenYe<UserInfotb> ChaoJi(string name="",int dangqianye = 1, int meiyetiaoshu = 5)
        {
            var list = from u in db.UserInfotb select u;
            if (!string.IsNullOrEmpty(name))
            {
                list = list.Where(p => p.UName.Contains(name));
            }
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

            if (zongtiaoshu == 0)
            {
                FenYe<UserInfotb> p = new FenYe<UserInfotb>();

                p.Zongtiaoshu = zongtiaoshu;
                p.Zongyeshu = page;
                p.Dangqianye = dangqianye;
                return p;
            }
            else
            {
                FenYe<UserInfotb> p = new FenYe<UserInfotb>();
                p.masd = list.Skip((dangqianye - 1) * meiyetiaoshu).Take(meiyetiaoshu).ToList();
                p.Zongtiaoshu = zongtiaoshu;
                p.Zongyeshu = page;
                p.Dangqianye = dangqianye;
                return p;
            }
        }
        /// <summary>
        /// 冻结账户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>> ZhuangTai1(int id )
        {
            UserInfotb b = db.UserInfotb.Find(id);
            b.UState = 2;
            return await db.SaveChangesAsync();
        }
        /// <summary>
        /// 解冻账户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>> ZhuangTai5(int id)
        {
            UserInfotb b = db.UserInfotb.Find(id);
            b.UState = 1;
            return await db.SaveChangesAsync();
        }
        /// <summary>
        /// 修改客户密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>> KeHuPwd(int id, string jpwd,string xpwd)
        {
            var list = from y in db.UserInfotb where y.UPwd==jpwd select y;
            int i = list.Count();
            if (i>0)
            {
                UserInfotb b = db.UserInfotb.Find(id);
                b.UPwd = xpwd;
                return await db.SaveChangesAsync();
            }
            else
            {
                return 0;
            }            
        }
        /// <summary>
        /// 维修员接单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>> ZhuangTai2(int id, int id2)
        {
            UserRepairsDetailstb b = db.UserRepairsDetailstb.Find(id2);
            b.State = 2;
            int i = db.SaveChanges();
            if (i>0)
            {
                UserInfotb u1 = db.UserInfotb.Find(id);
                Prompttb m = new Prompttb();
                m.PromptContent = "您的订单"+b.Ordernumber+"已被接受，当前维修员是" + u1.UName+"手机号码是"+u1.UPhone;
                m.PromptTime = DateTime.Now;
                m.UId = b.UId;
                m.UrdId = id2;
                m.PromptSet = 0;
                db.prompttb.Add(m);
                return await db.SaveChangesAsync();
            }
            else
            {
                return 0;
            }

        }
        /// <summary>
        /// 维修员维完成
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>> ZhuangTai4(int id,int uid)
        {
            UserRepairsDetailstb b = db.UserRepairsDetailstb.Find(id);
            b.State = 3;
            int i = db.SaveChanges();
            if (i > 0)
            {
                UserInfotb u1 = db.UserInfotb.Find(uid);
                Prompttb m = new Prompttb();
                m.PromptContent = "您的订单" + b.Ordernumber + "已完成，当前维修员是" + u1.UName + "手机号码是" + u1.UPhone;
                m.PromptTime = DateTime.Now;
                m.UId = b.UId;
                m.UrdId = id;
                m.PromptSet = 0;
                db.prompttb.Add(m);
                return await db.SaveChangesAsync();
            }
            else
            {
                return 0;
            }
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
        /// <summary>
        /// 反填
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<UserInfotb>>>Fan(int id)
        {
            var list = from u in db.UserInfotb select u;
            list = list.Where(p => p.UId == id);
            return await list.ToListAsync();
        }
        /// <summary>
        /// 修改资料
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>>ZL(int id,string name,string phone)
        {
            UserInfotb b = db.UserInfotb.Find(id);
            b.UName = name;
            b.UPhone = phone;
            return await db.SaveChangesAsync();
        }
        /// <summary>
        /// 添加个人维修单
        /// </summary>
        /// <param name="urdid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>> Add(int urdid,int uid)
        {
            MaintenanceDetailstb m = new MaintenanceDetailstb();
            m.URDId = urdid;
            m.UId = uid;
            db.MaintenanceDetailstb.Add(m);
            return await db.SaveChangesAsync();
        }

    }
}
