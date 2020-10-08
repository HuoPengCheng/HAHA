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
        public async Task<ActionResult<int>> Addwenjuan(string mingcheng)
        {
            wenjuan wenjuan = new wenjuan()
            {
                mingcheng = mingcheng,
                shijian = DateTime.Now.ToString()
            };
            db.Wenjuans.Add(wenjuan);
            return await db.SaveChangesAsync();
        }
        /// <summary>
        /// 添加题目和选项
        /// </summary>
        /// <param name="id"></param>
        /// <param name="biaoti"></param>
        /// <param name="xxneirong"></param>
        /// <param name="xxneirong1"></param>
        /// <param name="xxneirong2"></param>
        /// <param name="xxneirong3"></param>
        /// <returns></returns>
        [Route("Addtimu")]
        public async Task<ActionResult<int>> Addtimu(int id,string biaoti,string xxneirong, string xxneirong1, string xxneirong2, string xxneirong3)
        {
            timu timu2 = new timu();
            timu2.biaoti = biaoti;
            timu2.wj_id = id;
            db.Timus.Add(timu2);
            if (db.SaveChanges()>0)
            {
                timu timu1 = db.Timus.Where(x => x.biaoti == biaoti && x.wj_id == id).FirstOrDefault();
                int str = timu1.tmid;
                xuanxiang xuanxiang = new xuanxiang();
                xuanxiang.xuanxiangneirong = xxneirong;
                xuanxiang.piaoshu = 0;
                xuanxiang.tm_id = str;
                db.Xuanxiangs.Add(xuanxiang);
                if (db.SaveChanges() > 0)
                {
                    xuanxiang xuanxiang1 = new xuanxiang();
                    xuanxiang1.xuanxiangneirong = xxneirong1;
                    xuanxiang1.piaoshu = 0;
                    xuanxiang1.tm_id = str;
                    db.Xuanxiangs.Add(xuanxiang1);
                    if (db.SaveChanges() > 0)
                    {
                        xuanxiang xuanxiang2 = new xuanxiang();
                        xuanxiang2.xuanxiangneirong = xxneirong2;
                        xuanxiang2.piaoshu = 0;
                        xuanxiang2.tm_id = str;
                        db.Xuanxiangs.Add(xuanxiang2);

                        if (db.SaveChanges() > 0)
                        {
                            xuanxiang xuanxiang3 = new xuanxiang();
                            xuanxiang3.xuanxiangneirong = xxneirong3;
                            xuanxiang3.piaoshu = 0;
                            xuanxiang3.tm_id = str;
                            db.Xuanxiangs.Add(xuanxiang3);
                            return await db.SaveChangesAsync();

                        }
                        else
                        {
                            return 0;
                        }

                    }
                    else
                    {
                        return 0;
                    }

                }
                else
                {
                    return 0;
                }

            }
            else
            {
                return 0;
            }
            
        }
        /// <summary>
        /// 显示问卷信息
        /// </summary>
        /// <returns></returns>
        [Route("Getwenjuan")]
        public async Task<ActionResult<IEnumerable<wenjuan>>> Getwenjuan()
        {
            return await db.Wenjuans.ToListAsync();
        }
        [Route("GettmAll")]
        public async Task<ActionResult<IEnumerable<timuAll>>> GettmAll()
        {
            var list = from a in db.Timus
                       join b in db.Xuanxiangs
                       on a.tmid equals b.tm_id
                       select new timuAll()
                       {
                           tmid = a.tmid,
                           biaoti = a.biaoti,
                           xxid = b.xxid,
                           xuanxiangneirong = b.xuanxiangneirong,
                           piaoshu = b.piaoshu,
                           tm_id = b.tm_id
                       };
            return await list.ToListAsync();
        }

        /// <summary>
        /// 删除问卷信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Removewenjuan")]
        public async Task<ActionResult<int>> Removewenjuan(int id)
        {
            db.Wenjuans.Remove(db.Wenjuans.Find(id));
            return await db.SaveChangesAsync();
        }
        /// <summary>
        /// 添加意见投诉
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="Img"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddComplaints")]
        [Obsolete]
        public async Task<ActionResult<int>> AddComplaints(string comment, string Img, int state)
        {
            Complaintb complaintb = new Complaintb()
            {
                Comment = comment,
                Img = Img,
                State = 0
            };
            db.Complaintb.Add(complaintb);
            return await db.SaveChangesAsync();
        }
        //反填
        [Route("Fan")]
        public async Task<ActionResult<IEnumerable<timuAll>>> Fan(int id)
        {
            var list = from a in db.Wenjuans
                       join b in db.Timus
                       on a.wjid equals b.wj_id
                       join c in db.Xuanxiangs
                       on b.tmid equals c.tm_id
                       select new timuAll()
                       {
                           wjid = a.wjid,
                           //tmid = b.tmid,
                           biaoti = b.biaoti,
                           xuanxiangneirong = c.xuanxiangneirong,
                           mingcheng = a.mingcheng,
                           piaoshu = c.piaoshu
                           
                       };
            return await list.Where(p => p.wjid == id).ToListAsync();
        }
        [Route("uptPiao")]
        public async Task<ActionResult<int>> uptPiao(string name1,string name2,string name3,string name4)
        {
            xuanxiang xx1 = db.Xuanxiangs.Where(s => s.xuanxiangneirong == name1).FirstOrDefault();
            xx1.piaoshu++;
            db.Entry(xx1).State = EntityState.Modified;
            xuanxiang xx2 = db.Xuanxiangs.Where(s => s.xuanxiangneirong == name2).FirstOrDefault();
            xx2.piaoshu++;
            db.Entry(xx2).State = EntityState.Modified;
            xuanxiang xx3 = db.Xuanxiangs.Where(s => s.xuanxiangneirong == name3).FirstOrDefault();
            xx3.piaoshu++;
            db.Entry(xx3).State = EntityState.Modified;
            xuanxiang xx4 = db.Xuanxiangs.Where(s => s.xuanxiangneirong == name4).FirstOrDefault();
            xx4.piaoshu++;
            db.Entry(xx4).State = EntityState.Modified;
            return await db.SaveChangesAsync();

        }
    }
}