using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMS.PIGSNey.Models;

/// <summary>
/// 袁少峰  订单详情表
/// </summary>
namespace OMS.PIGSNey.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class TheorderdetailsController : ControllerBase
    {
        public OMSContext db;
        public TheorderdetailsController(OMSContext db) { this.db = db; }


        [HttpGet]
        [Route("api/UserGetURD")]
        public FenYe<UserRepairsDetailstb> UserGetURD(int UId, string Name = "", string StrTime = "", string EndTime = "", int PageSize = 5, int CurrPage = 1)
        {
            var linq = from u in db.UserRepairsDetailstb
                       orderby u.Degree descending
                       select new UserRepairsDetailstb
                       {
                           UId = u.UId,
                           UrdId = u.UrdId,
                           Ordernumber = u.Ordernumber,
                           Type = u.Type,
                           Degree = u.Degree,
                           Marque = u.Marque,
                           Cause = u.Cause,
                           Reason = u.Reason,
                           Address = u.Address,
                           Date = u.Date,
                           State = u.State,
                           DetailedAddress = u.DetailedAddress
                       };
            linq = linq.Where(p => p.UId == UId);
            if (!string.IsNullOrEmpty(Name))
            {
                linq = linq.Where(x => x.Ordernumber.Contains(Name));
            }
            if (!string.IsNullOrEmpty(StrTime))
            {
                DateTime st = DateTime.Parse(StrTime);
                linq = linq.Where(x => x.Date > st);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                DateTime et = DateTime.Parse(EndTime);
                linq = linq.Where(x => x.Date < et);
            }
            if (CurrPage < 1)
            {
                CurrPage = 1;
            }
            int TotalCount = linq.Count();
            int TotalPage = 0;
            if (TotalCount % PageSize == 0)
            {
                TotalPage = TotalCount / PageSize;
            }
            else
            {
                TotalPage = TotalCount / PageSize + 1;
            }
            FenYe<UserRepairsDetailstb> p = new FenYe<UserRepairsDetailstb>();
            p.masd = linq.Skip(PageSize * (CurrPage - 1)).Take(PageSize).ToList();
            p.Zongtiaoshu = TotalCount;
            p.Zongyeshu = TotalPage;
            p.Dangqianye = CurrPage;
            return p;
        }

        [HttpGet]
        [Route("api/GLYGetURD")]
        public FenYe<KeHuXianshi> GLYGetURD(string UserName = "", string StrTime = "", string EndTime = "", int PageSize = 5, int CurrPage = 1)
        {
            var linq = from a in db.UserRepairsDetailstb
                       join b in db.UserInfotb
                       on a.UId equals b.UId
                       select new KeHuXianshi()
                       {
                           UrdId = a.UrdId,
                           Ordernumber = a.Ordernumber,
                           Type = a.Type,
                           Marque = a.Marque,
                           Cause = a.Cause,
                           Reason = a.Reason,
                           Address = a.Address,
                           DetailedAddress = a.DetailedAddress,
                           Date = a.Date,
                           UId = a.UId,
                           State = a.State,
                           UName = b.UName,
                           UAccount = b.UAccount,
                           UPwd = b.UPwd,
                           UPhone = b.UPhone,
                           RId = b.RId,
                           UState = b.UState
                       };

            if (!string.IsNullOrEmpty(UserName))
            {
                linq.Where(x => x.UName.Equals(UserName));
            }
            if (!string.IsNullOrEmpty(StrTime))
            {
                DateTime st = DateTime.Parse(StrTime);
                linq.Where(x => x.Date > st);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                DateTime et = DateTime.Parse(EndTime);
                linq.Where(x => x.Date < et);
            }
            if (CurrPage < 1)
            {
                CurrPage = 1;
            }
            int TotalCount = linq.Count();
            int TotalPage = 0;
            if (TotalCount % PageSize == 0)
            {
                TotalPage = TotalCount / PageSize;
            }
            else
            {
                TotalPage = TotalCount / PageSize + 1;
            }
            FenYe<KeHuXianshi> p = new FenYe<KeHuXianshi>();
            p.masd = linq.Skip(PageSize * (CurrPage - 1)).Take(PageSize).ToList();
            p.Zongtiaoshu = TotalCount;
            p.Zongyeshu = TotalPage;
            p.Dangqianye = CurrPage;
            return p;
        }

        [HttpGet]
        [Route("api/ShenHe")]
        public async Task<ActionResult<int>> UpdateState(int UrdId, int UId)
        {
            UserRepairsDetailstb d = db.UserRepairsDetailstb.Find(UrdId);
            d.State = 1;
            db.Entry(d).State = EntityState.Modified;
            if (db.SaveChanges() < 0)
            {
                return 0;
            }
            else
            {
                Prompttb m = new Prompttb();
                m.PromptContent = "您的订单已被审核通过";
                m.PromptTime = DateTime.Now;
                m.UId = UId;
                m.UrdId = UrdId;
                db.prompttb.Add(m);
                return await db.SaveChangesAsync();
            }
        }
        [HttpGet]
        [Route("api/Bohui")]
        public async Task<ActionResult<int>> UpdateNoState(int UrdId, int UId)
        {
            UserRepairsDetailstb d = db.UserRepairsDetailstb.Find(UrdId);
            d.State = -1;
            db.Entry(d).State = EntityState.Modified;
            if (db.SaveChanges() < 0)
            {
                return 0;
            }
            else
            {
                Prompttb m = new Prompttb();
                m.PromptContent = "您的订单已被驳回，可以提供更多维修证据，进行二次提交哦";
                m.PromptTime = DateTime.Now;
                m.UId = UId;
                m.UrdId = UrdId;
                m.PromptSet = 0;
                db.prompttb.Add(m);
                return await db.SaveChangesAsync();
            }
        }



        [HttpGet]
        [Route("api/UserGetFT")]
        public List<UserRepairsDetailstb> UserGetFT(int UrdId)
        {
            return db.UserRepairsDetailstb.Where(x => x.UrdId == UrdId).ToList();
        }

        [HttpGet]
        [Route("api/UserDelURD")]
        public async Task<ActionResult<int>> UpdateState(int UrdId)
        {
            db.UserRepairsDetailstb.Remove(db.UserRepairsDetailstb.Find(UrdId));
            return await db.SaveChangesAsync();
        }

        [HttpGet]
        [Route("api/CommodityDetailstbGetType")]
        public List<CommodityDetailstb> CommodityDetailstbGetType()
        {
            return db.CommodityDetailstb.Where(x => x.CPId == 0).ToList();
        }
        [HttpGet]
        [Route("api/CommodityDetailstbGetMarque")]
        public List<CommodityDetailstb> CommodityDetailstbGetMarque(int CId)
        {
            return db.CommodityDetailstb.Where(x => x.CPId == CId).ToList();
        }

        [HttpGet]
        [Route("api/UserGetURDSH")]
        public FenYe<ViewModelUserRepairsDetailstb> UserGetURDSH(string Name = "", string StrTime = "", string EndTime = "", int PageSize = 5, int CurrPage = 1)
        {
            var linq = from u in db.UserRepairsDetailstb
                       join i in db.UserInfotb
                       on u.UId equals i.UId
                       orderby u.Degree descending
                       select new ViewModelUserRepairsDetailstb
                       {
                           UId = u.UId,
                           UrdId = u.UrdId,
                           Ordernumber = u.Ordernumber,
                           Type = u.Type,
                           Degree = u.Degree,
                           Marque = u.Marque,
                           Cause = u.Cause,
                           Reason = u.Reason,
                           Address = u.Address,
                           Date = u.Date,
                           State = u.State,
                           DetailedAddress = u.DetailedAddress,
                           UName = i.UName,
                           //用户账号
                           UAccount = i.UAccount,
                           //用户密码
                           UPwd = i.UPwd,
                           //手机号
                           UPhone = i.UPhone,
                           //角色id
                           RId = i.RId,
                           //状态
                           UState = i.UState
                       };
            if (!string.IsNullOrEmpty(Name))
            {
                linq = linq.Where(x => x.Ordernumber.Contains(Name));
            }
            if (!string.IsNullOrEmpty(StrTime))
            {
                DateTime st = DateTime.Parse(StrTime);
                linq = linq.Where(x => x.Date > st);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                DateTime et = DateTime.Parse(EndTime);
                linq = linq.Where(x => x.Date < et);
            }
            if (CurrPage < 1)
            {
                CurrPage = 1;
            }
            int TotalCount = linq.Count();
            int TotalPage = 0;
            if (TotalCount % PageSize == 0)
            {
                TotalPage = TotalCount / PageSize;
            }
            else
            {
                TotalPage = TotalCount / PageSize + 1;
            }
            FenYe<ViewModelUserRepairsDetailstb> p = new FenYe<ViewModelUserRepairsDetailstb>();
            p.masd = linq.Skip(PageSize * (CurrPage - 1)).Take(PageSize).ToList();
            p.Zongtiaoshu = TotalCount;
            p.Zongyeshu = TotalPage;
            p.Dangqianye = CurrPage;
            return p;
        }



        [HttpGet]
        [Route("api/GetResponse")]
        public List<ViewModelPrompttb> GetResponse(int UId)
        {
            
            var linq = from a in db.UserInfotb
                      join b in db.prompttb
                      on a.UId equals b.UId
                      join c in db.UserRepairsDetailstb
                      on b.UrdId equals c.UrdId
                      where b.UId.Equals(UId)
                      orderby b.PromptTime descending
                      select new ViewModelPrompttb
                      {
                          PRId = b.PRId,
                          PromptContent = b.PromptContent,
                          PromptTime = b.PromptTime,
                          UId = b.UId,
                          UrdId = b.UrdId,
                          Ordernumber=c.Ordernumber,
                          UName = a.UName,
                      };
            var pro = db.prompttb.Where(x => x.UId == UId).ToList();
            for (int i = 0; i < pro.Count(); i++)
            {
                Prompttb m = db.prompttb.Where(x => x.PRId == pro[i].PRId).FirstOrDefault();
                m.PromptSet = 1;
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
            }
            return linq.ToList();
        }

        [HttpGet]
        [Route("api/AddUserRepairsDetailstb")]
        public int AddUserRepairsDetailstb(string Type,string Marque,string Cause,string Reason,string Address,string DetailedAddress,int UId)
        {
            UserRepairsDetailstb m1 = (from a in db.UserRepairsDetailstb orderby a.UrdId descending select a).FirstOrDefault();

            UserRepairsDetailstb m = new UserRepairsDetailstb();
            m.Ordernumber = "XY2009-" + (m1.UrdId+1);
            m.Type = Type;
            m.Marque = Marque;
            m.Degree = 1;
            m.Cause = Cause;
            m.Reason = Reason;
            m.Address = Address;
            m.DetailedAddress = DetailedAddress;
            m.Date = DateTime.Now;
            m.UId = UId;
            m.State = 0;
            db.UserRepairsDetailstb.Add(m);
            return db.SaveChanges();
        }
        [HttpGet]
        [Route("api/PrompttbShow")]
        public int PrompttbShow(int UId)
        {
            var linq = db.prompttb.Where(x => x.UId == UId);
            int i = linq.Where(x => x.PromptSet == 0).Count()  ;
            return i;
        }



    }
}