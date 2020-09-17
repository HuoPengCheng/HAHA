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
    [Route("api/[controller]")]
    [ApiController]
    public class JurisdictionController : ControllerBase
    {
        public OMSContext db;
        public JurisdictionController(OMSContext db) { this.db = db; }

        
        [HttpGet]
        public async Task<ActionResult<int>> Denglu(string name,string pass)
        {
            db.UserInfotb.FirstOrDefault(x => x.UName == name).ToString();
            db.UserInfotb.FirstOrDefault(x => x.UPwd == pass).ToString();
            return await db.SaveChangesAsync();
        }
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
    }
}
