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
    [Route("api/[controller]")]
    [ApiController]
    public class ShuJuBaoBiaoController : ControllerBase
    {
        public OMSContext db;
        public ShuJuBaoBiaoController(OMSContext db)
        {
            this.db = db;
        }
        //查看问卷
        //有数据
        [HttpGet]
        [Route ("WenJuan")]
        public async Task<ActionResult<IEnumerable<wenjuan>>> WenJuan()
        {
            {
                var list = db.Wenjuans;
                return await list.ToListAsync();
            }
        }
        [HttpGet]
        //维修工单
        //有数据
        [Route("WeiXiu")]
        public string WeiXiu()
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
        //[HttpGet]
        //[Route ("complaints")]
        //[Obsolete]
        //public List<Complaintb>ShowXingJi(int pageIndex = 1, int pageSize = 10)
        //{


        //}
        //public async Task<ActionResult<int>> AddComplaints(string comment)
        //{
        //    Complaintb complaintb = new Complaintb()
        //    {
        //        Comment = comment,

        //    };    
        //    db.Complaintb.Add(complaintb);
        //    return await db.SaveChangesAsync();
        //}
    }
}