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
    //数据报表——API
    [Route("api/[controller]")]
    [ApiController]
    //问卷查看--作者/李达凯
    public class QuestiontbController : ControllerBase
    {
        public OMSContext db;
        public QuestiontbController(OMSContext db) { this.db = db; }



        //查看问卷
        [HttpGet]
        //[Route("api/question")]
        //连接意见投诉模块，添加问卷
        public async Task<ActionResult<IEnumerable<wenjuan>>> Wenjuan()
        {         
                var list = db.Wenjuans;
                return await list.ToListAsync();
        }
        [HttpGet]
        //维修工单
        //[Route("WeiXui")]
        public string WeiXui()
        {
            //分为四种状态：未审核、未维修、已完成、维修中
            //连接UserRepairsDetailstb（用户报修信息详情表）
            int a = db.UserRepairsDetailstb.Where(x => x.State == 1).Count();
            int b = db.UserRepairsDetailstb.Where(x => x.State == 2).Count();
            int c = db.UserRepairsDetailstb.Where(x => x.State == 3).Count();
            int d = db.UserRepairsDetailstb.Where(x => x.State == 4).Count();
            //然后在前台使用切割，返回
            //通过JSON返回String值
            string respon = a.ToString() + ',' + b.ToString() + ',' + c.ToString() + ',' + d.ToString();
            return respon;
        }

        //JS星级评分
        public async Task<ActionResult<int>> AddComplaints(string comment)
        {
            Complaintb complaintb = new Complaintb()
            {
                Comment = comment,

            };
            db.Complaintb.Add(complaintb);
            return await db.SaveChangesAsync();
        }

    }
}
