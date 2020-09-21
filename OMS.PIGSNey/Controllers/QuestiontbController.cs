using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OMS.PIGSNey.Models;

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
        public async Task<ActionResult<IEnumerable<Questiontb>>> question(string acount)
        {
            {
                var list = from q in db.Questiontb
                           join r in db.Questiontb
                           on q.QId equals r.QId
                           select new Questiontb()
                           {
                               Question1 = q.Question1,
                               Question2 = q.Question2,
                               Question3 = q.Question3,
                               Question4 = q.Question4,
                               QNumber = q.QNumber

                           };

                return await list.ToListAsync();
            }


        }

    }
}