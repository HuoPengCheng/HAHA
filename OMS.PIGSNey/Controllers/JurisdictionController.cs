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
        public async Task<ActionResult<int>> Denglu(string name,string pass)
        {
            db.UserInfotb.FirstOrDefault(x => x.UName == name).ToString();
            db.UserInfotb.FirstOrDefault(x => x.UPwd == pass).ToString();
            return await db.SaveChangesAsync();
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
        public FenYe<Jurisdiction> PageUserShow(int PageSize = 3, int CurrPage = 1)
        {
              var linq=from ro in db.Roletb join
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
            if (CurrPage < 1)
            {
                CurrPage = 1;
            }
            int totalcount = linq.ToList().Count();
            int totalpage;
            if (totalcount % PageSize == 0)
            {
                totalpage = totalcount / PageSize;
            }
            else
            {
                totalpage = totalcount / PageSize + 1;
            }
            if (CurrPage > totalpage)
            {
                CurrPage = totalpage;
            }
            FenYe<Jurisdiction> p1 = new FenYe<Jurisdiction>();
            p1.masd = linq.Skip(PageSize * (CurrPage - 1)).Take(PageSize).ToList();
            p1.Zongyeshu = CurrPage;
            p1.Zongtiaoshu = totalcount;
            p1.Dangqianye = totalpage;
            return p1;
        }
    }
}
