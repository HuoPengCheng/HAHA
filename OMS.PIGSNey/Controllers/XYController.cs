using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.PIGSNey.Models;

namespace OMS.PIGSNey.Controllers
{
    /// <summary>
    /// 张鑫鑫
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class XYController : ControllerBase
    {
        public OMSContext db;
        public XYController(OMSContext db)
        {
            this.db = db;
        }

        #region  材料审核
        /// <summary>
        /// 材料审核
        /// </summary>
        /// <param name="MaterialName"></param>
        /// <param name="UName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        
        [Route("ApplyFortbShow")]
        public PageAF ApplyFortbShow(string MaterialName="",string UName="",int pageIndex=1,int pageSize = 2)
        {
            if (pageIndex<1)
            {
                pageIndex = 1;
            }
            var list = (from a in db.ApplyFortb
                        join u in db.UserInfotb on a.UId equals u.UId
                        orderby a.AppDate descending
                        select new AF
                        {
                            AId=a.AId,
                            MaterialName = a.MaterialName,
                            MaterialAmount = a.MaterialAmount,
                            UName = u.UName,
                            AppDate = a.AppDate,
                            AStatic = a.AStatic
                        });
             if (!string.IsNullOrEmpty(MaterialName))
            {
                list = list.Where(a => a.MaterialName.Contains(MaterialName));
            }
            if (!string.IsNullOrEmpty(UName))
            {
                list = list.Where(u => u.UName.Contains(UName));
            }

            int count = list.Count();
            var totalpage = count / pageSize + (count % pageSize == 0 ? 0 : 1);
            if (pageIndex>totalpage)
            {
                pageIndex = totalpage;
            }
            PageAF pageaf = new PageAF();
            pageaf.totalCount = count;
            pageaf.totalPage = totalpage;
            pageaf.PageIndex = pageIndex;
            pageaf.AF = list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return pageaf;

        }

        #endregion
    }
}