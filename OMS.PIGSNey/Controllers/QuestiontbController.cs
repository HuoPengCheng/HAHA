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
    [Route("api/[controller]")]
    [ApiController]
    //问卷查看---作者/李达凯
    public class QuestiontbController : ControllerBase
    {
        public OMSContext db;
        public QuestiontbController(OMSContext db) { this.db = db; }
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
        [HttpGet]
        //维修工单
        [Route("WeiXui")]
        public async Task<ActionResult<IEnumerable<KeHuXianshi>>> WeiXui(int mid)
        {
            var list = from u in db.MaintenanceDetailstb
                       join r in db.Materialstb
                       on u.URDId equals r.MAId
                       join m in db.MaintenanceDetailstb
                       on r.MAId equals m.URDId
                       select new KeHuXianshi
                       {
                          
                       };
            list = list.Where(p => p.MId == mid && p.State == 2 || p.State == 3 || p.State == 4);
            return await list.ToListAsync();

        }
  

    }
}