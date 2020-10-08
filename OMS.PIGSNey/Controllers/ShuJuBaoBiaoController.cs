using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;             //引用
using Microsoft.AspNetCore.Mvc;              //引用
using Microsoft.EntityFrameworkCore;         //引用
using OMS.PIGSNey.Models;                    //引用
using Newtonsoft.Json;                       //引用
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;//引用
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
        [Route("WenJuan")]
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
            int a = db.UserRepairsDetailstb.Where(x => x.State == 0).Count();
            int b = db.UserRepairsDetailstb.Where(x => x.State == 1).Count();
            int c = db.UserRepairsDetailstb.Where(x => x.State == 2).Count();
            int d = db.UserRepairsDetailstb.Where(x => x.State == 3).Count();
            int e = db.UserRepairsDetailstb.Where(x => x.State == -1).Count();
            //然后在前台使用切割，返回
            //通过JSON返回String值
            string respon = a.ToString() + ',' + b.ToString() + ',' + c.ToString() + ',' + d.ToString() + ',' + e.ToString();
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


        [HttpGet]
        //维修工单
        //有数据
        [Route("WeiXiuNum")]
        public string WeiXiuNum()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string Name = "";
            string Num = "";
            var linq = from a in db.UserInfotb
                       join b in db.Roletb
                       on a.RId equals b.RId
                       where b.RId == 3
                       select new ViewUserRole()
                       {
                           UId = a.UId,
                           //姓名
                           UName = a.UName,
                           //用户账号
                           UAccount = a.UAccount,
                           //用户密码
                           UPwd = a.UPwd,
                           //手机号
                           UPhone = a.UPhone,
                           //角色id
                           RId = a.RId,
                           //状态
                           UState = a.UState,
                           //角色名称
                           RName = b.RName
                       };

            for (int i = 0; i < linq.ToList().Count(); i++)
            {
                var sele = from a in db.UserInfotb
                           join b in db.MaintenanceDetailstb
                           on a.UId equals b.UId
                           join c in db.UserRepairsDetailstb
                           on b.URDId equals c.UrdId
                           where a.UName.Equals(linq.ToList()[i].UName)
                           select new UserInfotb()
                           {
                               UName = a.UName
                           };
                if (sele.ToList().Count() == 0)
                {
                    Name += "" + linq.ToList()[i].UName + "" + ",";
                    Num += "0,";
                    dic.Add(Name, Num);
                }
                else
                {
                    Name += "" + linq.ToList()[i].UName + "" + ',';

                    Num += (sele.ToList().Count()) + ",";

                }

            }
            string All = Name.Trim(',') + ':' + Num.Trim(',');
            return All;

        }

        [HttpGet]
        //维修工单
        //有数据
        [Route("Ces")]
        public string Ces()
        {
            var linq = from a in db.UserInfotb
                       join b in db.Roletb
                       on a.RId equals b.RId
                       where b.RId == 3
                       select new ViewUserRole()
                       {
                           UId = a.UId,
                           //姓名
                           UName = a.UName,
                           //用户账号
                           UAccount = a.UAccount,
                           //用户密码
                           UPwd = a.UPwd,
                           //手机号
                           UPhone = a.UPhone,
                           //角色id
                           RId = a.RId,
                           //状态
                           UState = a.UState,
                           //角色名称
                           RName = b.RName
                       };
            var sele = from a in db.UserInfotb
                       join b in db.MaintenanceDetailstb
                       on a.UId equals b.UId
                       join c in db.UserRepairsDetailstb
                       on b.URDId equals c.UrdId
                       where a.UName.Equals(linq.ToList()[2].UName)
                       select new UserInfotb()
                       {
                           UName = a.UName
                       };

            return sele.ToList()[0].UName;
        }

        //导出


        //[HttpGet]
        //[Route("api/GLYGetURD")]
        //public FenYe<KeHuXianshi> GLYGetURD(string UserName = "", string StrTime = "", string EndTime = "", int PageSize = 5, int CurrPage = 1)
        //{
        //    var linq = from a in db.UserRepairsDetailstb
        //               join b in db.UserInfotb
        //               on a.UId equals b.UId
        //               select new KeHuXianshi()
        //               {
        //                   UrdId = a.UrdId,
        //                   Ordernumber = a.Ordernumber,
        //                   Type = a.Type,
        //                   Marque = a.Marque,
        //                   Cause = a.Cause,
        //                   Reason = a.Reason,
        //                   Address = a.Address,
        //                   DetailedAddress = a.DetailedAddress,
        //                   Date = a.Date,
        //                   UId = a.UId,
        //                   State = a.State,
        //                   UName = b.UName,
        //                   UAccount = b.UAccount,
        //                   UPwd = b.UPwd,
        //                   UPhone = b.UPhone,
        //                   RId = b.RId,
        //                   UState = b.UState
        //               };

        //    if (!string.IsNullOrEmpty(UserName))
        //    {
        //        linq.Where(x => x.UName.Equals(UserName));
        //    }
        //    if (!string.IsNullOrEmpty(StrTime))
        //    {
        //        DateTime st = DateTime.Parse(StrTime);
        //        linq.Where(x => x.Date > st);
        //    }
        //    if (!string.IsNullOrEmpty(EndTime))
        //    {
        //        DateTime et = DateTime.Parse(EndTime);
        //        linq.Where(x => x.Date < et);
        //    }
        //    if (CurrPage < 1)
        //    {
        //        CurrPage = 1;
        //    }
        //    int TotalCount = linq.Count();
        //    int TotalPage = 0;
        //    if (TotalCount % PageSize == 0)
        //    {
        //        TotalPage = TotalCount / PageSize;
        //    }
        //    else
        //    {
        //        TotalPage = TotalCount / PageSize + 1;
        //    }
        //    FenYe<KeHuXianshi> p = new FenYe<KeHuXianshi>();
        //    p.masd = linq.Skip(PageSize * (CurrPage - 1)).Take(PageSize).ToList();
        //    p.Zongtiaoshu = TotalCount;
        //    p.Zongyeshu = TotalPage;
        //    p.Dangqianye = CurrPage;
        //    return p;
        //}
        //添加投诉信息
        [Route("api/AddTouSu")]

        public async Task<ActionResult<int>> AddTouSu(string Ordernumber, string UId1, string UId2, string Comment, string Img, int State)
        {
            Complaintb TouSu = new Complaintb()
            {

                Ordernumber = Ordernumber,
                UName1 = UId1,
                UName2 = UId2,
                Comment = Comment,
                Img = Img,
                State = State
            };
            db.Complaintb.Add(TouSu);
            return await db.SaveChangesAsync();
        }
        /// <summary>
        /// 显示投诉信息
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("GetTouSu")]
        [HttpGet]
        public FenYe<Complaintb> GetTouSu(int PageSize=5,int CurrPage=1)
        {
            if (CurrPage<1)
            {
                CurrPage = 1;
            }
            var linq = db.Complaintb;
            int totalCount = linq.Count();
            int totalPage;
            if (totalCount%PageSize==0)
            {
                totalPage = totalCount / PageSize;
            }
            else
            {
                totalPage = totalCount / PageSize+1;
            }
            if (CurrPage>totalPage)
            {
                CurrPage = totalPage;
            }
            FenYe<Complaintb> f = new FenYe<Complaintb>();
            f.masd = linq.Skip(PageSize * (CurrPage - 1)).Take(PageSize).ToList();
            f.Zongtiaoshu = totalCount;
            f.Zongyeshu = totalPage;
            f.Dangqianye = CurrPage;
            return f;
        }


        [Route("UpDateOK")]
        [HttpGet]
        public int UpDateOK(int CoId)
        {
            Complaintb c = db.Complaintb.Find(CoId);
            c.State = 1;
            db.Entry(c).State = EntityState.Modified;
            if (db.SaveChanges() < 0)
            {
                return 0;
            }
            else
            {
                string ordernumber = c.Ordernumber;
                UserRepairsDetailstb u = db.UserRepairsDetailstb.Where(x => x.Ordernumber == ordernumber).FirstOrDefault();
                int urdid = u.UrdId;
                int uid = u.UId;
                Prompttb m = new Prompttb();
                m.PromptContent = "您的投诉申请已审核成功对您的不便请谅解";
                m.PromptTime = DateTime.Now;
                m.UId = uid;
                m.UrdId = urdid;
                db.prompttb.Add(m);
                return db.SaveChanges();
            }

        }


        [Route("UpDateNO")]
        [HttpGet]
        public int UpDateNO(int CoId)
        { 
            Complaintb c = db.Complaintb.Find(CoId);
            c.State = 2;  
            db.Entry(c).State = EntityState.Modified;
            if (db.SaveChanges() < 0)
            {
                return 0; 
            }  
            else 
            {
                string ordernumber = c.Ordernumber;
                UserRepairsDetailstb u = db.UserRepairsDetailstb.Where(x => x.Ordernumber == ordernumber).FirstOrDefault();
                int urdid = u.UrdId;
                int uid = u.UId;
                Prompttb m = new Prompttb();
                m.PromptContent = "您的投诉申请被驳回请提供更多证据";
                m.PromptTime = DateTime.Now;
                m.UId = uid;
                m.UrdId = urdid;
                db.prompttb.Add(m);
                return db.SaveChanges();
            }

        }








    }

}
