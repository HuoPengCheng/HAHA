using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OMS.PIGSNey.Models;

/// <summary>
/// 袁少峰  订单详情表
/// </summary>
namespace OMS.PIGSNey.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class TheorderdetailsController : ControllerBase
    {
        public OMSContext db;
        public TheorderdetailsController(OMSContext db) { this.db = db; }


        [HttpGet]
        [Route("api/UserGetURD")]
        public FenYe<UserRepairsDetailstb> UserGetURD(int UId, string Name = "", string StrTime = "", string EndTime = "", int PageSize = 5, int CurrPage = 1)
        {
            var linq = db.UserRepairsDetailstb.Where(x => x.UId == UId).ToList();
            if (!string.IsNullOrEmpty(Name))
            {
                linq.Where(x => x.Type == Name);
            }
            if (!string.IsNullOrEmpty(StrTime))
            {
                DateTime st = DateTime.Parse(StrTime);
                linq.Where(x => x.Date > st);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                DateTime et = DateTime.Parse(EndTime);
                linq.Where(x => x.Date < et);
            }
            if (CurrPage < 1)
            {
                CurrPage = 1;
            }
            int TotalCount = linq.Count();
            int TotalPage = 0;
            if (TotalCount % PageSize == 0)
            {
                TotalPage = TotalCount / PageSize;
            }
            else
            {
                TotalPage = TotalCount / PageSize + 1;
            }
            FenYe<UserRepairsDetailstb> p = new FenYe<UserRepairsDetailstb>();
            p.masd = linq.Skip(PageSize * (CurrPage - 1)).Take(PageSize).ToList();
            p.Zongtiaoshu = TotalCount;
            p.Zongyeshu = TotalPage;
            p.Dangqianye = CurrPage;
            return p;
        }

        [HttpGet]
        [Route("api/GLYGetURD")]
        public FenYe<KeHuXianshi> GLYGetURD(string UserName = "", string StrTime = "", string EndTime = "", int PageSize = 5, int CurrPage = 1)
        {
            var linq = from a in db.UserRepairsDetailstb
                       join b in db.UserInfotb
                       on a.UId equals b.UId
                       select new KeHuXianshi()
                       {
                           UrdId = a.UrdId,
                           Ordernumber = a.Ordernumber,
                           Type = a.Type,
                           Marque = a.Marque,
                           Cause = a.Cause,
                           Reason = a.Reason,
                           Address = a.Address,
                           DetailedAddress = a.DetailedAddress,
                           Date = a.Date,
                           UId = a.UId,
                           State = a.State,
                           UName=b.UName,
                           UAccount=b.UAccount,
                           UPwd = b.UPwd,
                           UPhone=b.UPhone,
                           RId=b.RId,
                           UState=b.UState
                       };

            if (!string.IsNullOrEmpty(UserName))
            {
                linq.Where(x => x.UName.Equals(UserName));
            }
            if (!string.IsNullOrEmpty(StrTime))
            {
                DateTime st = DateTime.Parse(StrTime);
                linq.Where(x => x.Date > st);
            }
            if (!string.IsNullOrEmpty(EndTime))
            {
                DateTime et = DateTime.Parse(EndTime);
                linq.Where(x => x.Date < et);
            }
            if (CurrPage < 1)
            {
                CurrPage = 1;
            }
            int TotalCount = linq.Count();
            int TotalPage = 0;
            if (TotalCount % PageSize == 0)
            {
                TotalPage = TotalCount / PageSize;
            }
            else
            {
                TotalPage = TotalCount / PageSize + 1;
            }
            FenYe<KeHuXianshi> p = new FenYe<KeHuXianshi>();
            p.masd = linq.Skip(PageSize * (CurrPage - 1)).Take(PageSize).ToList();
            p.Zongtiaoshu = TotalCount;
            p.Zongyeshu = TotalPage;
            p.Dangqianye = CurrPage;
            return p;
        }




    }
}
