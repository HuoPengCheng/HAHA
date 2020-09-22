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
    /// <summary>
    /// 张家旺   意见投诉
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class ComplaintsController : ControllerBase
    {
        public OMSContext db;
        public ComplaintsController(OMSContext db) { this.db = db; }
        /// <summary>
        /// 添加问卷信息
        /// </summary>
        /// <param name="mingcheng"></param>
        /// <param name="shijian"></param>
        /// <returns></returns>
        [Route("Addwenjuan")]
        public async Task<ActionResult<int>> Addwenjuan(string mingcheng, string shijian)
        {
            wenjuan wenjuan = new wenjuan()
            {
                mingcheng = mingcheng,
                shijian = shijian
            };
            db.wenjuan.Add(wenjuan);
            return await db.SaveChangesAsync();
        }
        /// <summary>
        /// 显示问卷信息
        /// </summary>
        /// <returns></returns>
        [Route("Getwenjuan")]
        public async Task<ActionResult<IEnumerable<wenjuan>>> Getwenjuan()
        {
            return await db.wenjuan.ToListAsync();
        }
        /// <summary>
        /// 删除问卷信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult<int>> Removewenjuan(int id)
        {
            db.wenjuan.Remove(db.wenjuan.Find(id));
            return await db.SaveChangesAsync();
        }
        //[Obsolete]
        //public int toupiaoAdd()
        //{

        //}
    }
}