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
                      UName=u.UName,
                      UAccount=u.UAccount,
                      UPwd=u.UPwd,
                      UPhone=u.UPhone,
                      UState=u.UState,
                  };
            var i = p.Where(x => x.UName == name && x.UPwd == pass || x.UPhone == name && x.UPwd == pass || x.UAccount == name && x.UPwd == pass).ToListAsync();
            return await i;
        }
         
    }
}
