using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OMS.PIGSNey.Models;
using Microsoft.AspNetCore.Cors;

namespace OMS.PIGSNey.Controllers
{
    //数据报表——API
    [Route("api/[controller]")]
    [ApiController]
    //问卷查看---作者/李达凯
    public class Questiontb : ControllerBase
    {
        public OMSContext db;
        public Questiontb(OMSContext db) { this.db = db; }
        //查看问卷
       [HttpGet]
        [Route("question")]
        public async Task<ActionResult<IEnumerable<wenjuan>>>  wenjuan()
        {
            {
                var list = db.Wenjuans;              
                return await list.ToListAsync();
            }


        }
        //[HttpGet]
        ////维修工单
        //[Route("WeiXui")]
        //public async Task<ActionResult<IEnumerable<string>>> WeiXui()
        //{
        //    int a = db.UserRepairsDetailstb.Where(x => x.State == 1).Count();
        //    int b = db.UserRepairsDetailstb.Where(x => x.State == 2).Count();
        //    int c = db.UserRepairsDetailstb.Where(x => x.State == 3).Count();
        //    int d = db.UserRepairsDetailstb.Where(x => x.State == 4).Count();

          
        //    string respon = "";

        //}
  
       

    }
}