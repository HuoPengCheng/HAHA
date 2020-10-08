using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.PIGSNey.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace OMS.PIGSNey.Controllers
{
    /// <summary>
    /// 郑 权限管理
    /// </summary>
    [EnableCors("cors")]
    [ApiController]
    public class JurisdictionController : ControllerBase
    {
        public OMSContext db;
        public JurisdictionController(OMSContext db) { this.db = db; }

        
        [HttpGet]
        [Route("api/Denglu")]
        public int Denglu(string name,string pass)
        {
            
            int i =  db.UserInfotb.Where(x => x.UName == name && x.UPwd == pass || x.UPhone == name && x.UPwd == pass || x.UAccount == name && x.UPwd == pass).Count();
            return i;
        }
        [Route("api/UserShow")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jurisdiction>>> UserShow()
        {
            var p=from ro in db.Roletb join
                  u in db.UserInfotb on
                  ro.RId equals u.RId
                  select new Jurisdiction
                  {
                      RName=ro.RName,
                      UName=u.UName,
                      UAccount=u.UAccount,
                      UPwd=u.UPwd,
                      UPhone=u.UPhone,
                      UState=u.UState,
                  };
            return await p.ToListAsync();
        }
        [Route("api/PageUserShow")]
        [HttpGet]
        public FenYe<Jurisdiction> GLYGetURD(string Name = "",string sjh="",string zh="", int PageSize = 5, int CurrPage = 1)
        {
           var linq=from ro in db.Roletb join
                  u in db.UserInfotb on
                  ro.RId equals u.RId
                  select new Jurisdiction
                  {
                      UId=u.UId,
                      RName=ro.RName,
                      UName=u.UName,
                      UAccount=u.UAccount,
                      UPwd=u.UPwd,
                      UPhone=u.UPhone,
                      UState=u.UState,
                  };

            if (!string.IsNullOrWhiteSpace(Name))
            {
                linq = linq.Where(x => x.UName.Contains(Name));
            }

            if (!string.IsNullOrWhiteSpace(sjh))
            {
                linq = linq.Where(x => x.UAccount.Contains(sjh));
            }

            if (!string.IsNullOrWhiteSpace(zh))
            {
                linq = linq.Where(x => x.UPhone.Contains(zh));
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
            FenYe<Jurisdiction> p = new FenYe<Jurisdiction>();
            p.masd = linq.Skip(PageSize * (CurrPage - 1)).Take(PageSize).ToList();
            p.Zongtiaoshu = TotalCount;
            p.Zongyeshu = TotalPage;
            p.Dangqianye = CurrPage;
            return p;
        }

        [HttpDelete]
        [Route("api/gai")]
        public int Gai(int id)
        {
            UserInfotb u = db.UserInfotb.FirstOrDefault(x => x.UId == id);
            if (u.UState==1)
            {
                u.UState = 2;
            }
            else
            {
                u.UState = 1;
            }
            return  db.SaveChanges();
        }

        [HttpGet]
        [Route("api/Session")]
        public async Task<ActionResult<IEnumerable<Jurisdiction>>> Session(string name,string pass)
        {
            var p=from ro in db.Roletb join
                  u in db.UserInfotb on
                  ro.RId equals u.RId
                  select new Jurisdiction
                  {
                      UId=u.UId,
                      RName=ro.RName,
                      RId=ro.RId,
                      UName=u.UName,
                      UAccount=u.UAccount,
                      UPwd=u.UPwd,
                      UPhone=u.UPhone,
                      UState=u.UState,
                  };
            var i = p.Where(x => x.UName == name && x.UPwd == pass || x.UPhone == name && x.UPwd == pass || x.UAccount == name && x.UPwd == pass).ToListAsync();
            return await i;
        }
         
        //public int Add(string name,string uAccount,string upwd,string uphone,int rid,int ustate)
        //{
        //    UserInfotb u = new UserInfotb();
        //    u.UName = name;
        //    u.UAccount = uAccount;
        //    u.UPwd = upwd;
        //    u.UPhone = uphone;
        //    u.RId = 4;
        //    u.UState = 1;
        //    db.Add(u);
        //    return db.SaveChanges();


        //}
        
        [HttpGet]
        [Route("api/qx")]
        public async Task<ActionResult<IEnumerable<Jurisdiction>>> Qx(string name,string pass)
        {
            var linq=from ro in db.Roletb join
                  j in db .Juristb on
                  ro.RId equals j.RId
                  select new Jurisdiction
                  {
                      RName=ro.RName,
                      JId=j.JId,
                      RId=j.RId,
                      JAdd=j.JAdd,
                      JDel=j.JDel,
                      JShow=j.JShow,
                      JUpt=j.JUpt,

                  };


            return await linq.ToListAsync();
        }

        [HttpGet]
        [Route("api/Moban")]
        public async Task<ActionResult<IEnumerable<Qx>>> Lindex(int rid)
        {
            if (rid==1)
            {
                 var linq=from ro in db.Roletb join
                  c in db .Cjtb on
                  ro.RId equals c.RId
                  join p in db.Permissionspage on
                  c.PId equals p.PId
                  select new Qx
                  {
                      PId=p.PId,
                      RName=ro.RName,
                      PName=p.PName,

                  };
                return await linq.ToListAsync();

            }
            else if(rid==2)
            {
                var linq = from ro in db.Roletb
                           join c in db.Gjtb on
                            ro.RId equals c.RId
                           join p in db.GPermission on
                           c.GId equals p.GPId
                           select new Qx
                           {
                               PId = p.GPId,
                               RName = ro.RName,
                               PName = p.PName,

                           };
                return await linq.ToListAsync();
            }
            else if (rid==3)
            {
                 var linq=from ro in db.Roletb join
                  c in db .Wxtb on
                  ro.RId equals c.rid
                  join p in db.WPermission on
                  c.wid equals p.WId
                  select new Qx
                  {
                      PId = p.WId,
                      RName =ro.RName,
                      PName = p.PName,
                  };
                return await linq.ToListAsync();
            }
             else 
            {
                 var linq=from ro in db.Roletb join
                  c in db .Khtb on
                  ro.RId equals c.RId
                  join p in db.YPermission on
                  c.KId equals p.PYId
                  select new Qx
                  {
                      PId = p.PYId,
                      RName =ro.RName,
                      PName = p.PName,
                  };
                return await linq.ToListAsync();
            }
           

        }
        [HttpGet]
        [Route("api/ej")]
        public async Task<ActionResult<IEnumerable<Qx>>> eji(int rid,int id)
        {
            if (rid==4)
            {
                 var linq=from ro in db.Roletb join
                  c in db .Cjtb on
                  ro.RId equals c.RId
                  join p in db.Permissionspage on
                  c.PId equals p.Id where p.Id==id && p.SId==1
                  select new Qx
                  {
                      RName=ro.RName,
                      PName=p.PName,
                      Url=p.Url,

                  };
 
                return await linq.ToListAsync();
            }
            else
            {
                 var linq=from ro in db.Roletb join
                  c in db .Cjtb on
                  ro.RId equals c.RId
                  join p in db.Permissionspage on
                  c.PId equals p.Id where p.Id==id
                  select new Qx
                  {
                      RName=ro.RName,
                      PName=p.PName,
                      Url=p.Url,

                  };
 
                return await linq.ToListAsync();
            }

                

        }
        

    }
}
