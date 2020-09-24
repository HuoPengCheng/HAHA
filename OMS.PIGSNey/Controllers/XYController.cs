using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OMS.PIGSNey.Models;
using Microsoft.EntityFrameworkCore;

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

        #region 类别（下拉框）

        [Route("GetCategory")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            var list = from c in db.Category select c;
            return await db.Category.ToListAsync();
        }

        #endregion

        #region 材料下拉框（材料名称）

        [Route("GetMaterials")]
        public async Task<ActionResult<IEnumerable<Materialstb>>> GetMaterials()
        {
            var list = from m in db.Materialstb 
                       select new Materialstb
                       {
                            MAId=m.MAId,
                            MaterialName = m.MaterialName
                        };
            return await db.Materialstb.ToListAsync();
        }

        #endregion

        #region 工具下拉框（工具名称）

        [Route("GetTool")]
        public async Task<ActionResult<IEnumerable<Tooltb>>> GetTool()
        {
            var list = from t in db.Tooltb
                       select new Tooltb
                       {
                           TId = t.TId,
                           ToolName = t.ToolName
                       };
            return await db.Tooltb.ToListAsync();
        }

        #endregion

        #region  材料审核显示
        /// <summary>
        /// 材料审核
        /// </summary>
        /// <param name="MaterialName"></param>
        /// <param name="UName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("ApplyFortbShow")]
        public FenYe<AF> ApplyFortbShow(string MaterialName = "", string UName = "", int pageIndex = 1, int pageSize = 2)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            var list = (from a in db.ApplyFortb
                        join u in db.UserInfotb on a.UId equals u.UId
                        join m in db.Materialstb on a.MAId equals m.MAId
                        orderby a.AppDate descending
                        where a.AStatic == 0
                        select new AF
                        {
                            AId = a.AId,
                            MAId=m.MAId,
                            MaterialName = m.MaterialName,
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
            if (pageIndex > totalpage)
            {
                pageIndex = totalpage;
            }
            FenYe<AF> fenYeAF = new FenYe<AF>();
            fenYeAF.Zongtiaoshu = count;
            fenYeAF.Zongyeshu = totalpage;
            fenYeAF.Dangqianye = pageIndex;
            fenYeAF.masd = list.Skip((pageIndex-1) * pageSize).Take(pageSize).ToList();

            return fenYeAF;
        }



        #endregion

        #region 工具审核显示
        /// <summary>
        /// 工具审核显示
        /// </summary>
        /// <param name="ToolName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("AddTool")]
        public FenYe<AT> AddTool(string ToolName = "", string UName = "", int pageIndex = 1, int pageSize = 2)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            var list = (from at in db.AddTool
                        join u in db.UserInfotb on at.UId equals u.UId
                        join t in db.Tooltb on at.TId equals t.TId
                        orderby at.AppDate descending
                        where at.AStatic == 0
                        select new AT
                        {
                            ATId = at.ATId,
                            ToolName = t.ToolName,
                            UName = u.UName,
                            AppDate = at.AppDate
                        }) ;
            if (!string.IsNullOrEmpty(ToolName))
            {
                list = list.Where(a => a.ToolName.Contains(ToolName));
            }
            if (!string.IsNullOrEmpty(UName))
            {
                list = list.Where(s => s.UName.Contains(UName));
            }
            int count = list.Count();
            var totalpage = count / pageSize + (count % pageSize == 0 ? 0 : 1);
            if (pageIndex > totalpage)
            {
                pageIndex = totalpage;
            }
            FenYe<AT> fenYeAT = new FenYe<AT>();
            fenYeAT.Zongtiaoshu = count;
            fenYeAT.Zongyeshu = totalpage;
            fenYeAT.Dangqianye = pageIndex;
            fenYeAT.masd = list.Skip((pageIndex-1) * pageSize).Take(pageSize).ToList();

            return fenYeAT;
        }

        #endregion

        #region 审核

        /// <summary>
        /// 根据AID查找数据进项审核
        /// </summary>
        /// <param name="AId"></param>
        /// <returns></returns>

        [Route("UpdAf")]
        public async Task<ActionResult<int>> UpdAf(int AId,int MAId)
        {
            
            ApplyFortb af = db.ApplyFortb.Find(AId);
            af.AStatic = 1;
            int num = af.MaterialAmount;
            db.Entry(af).State = EntityState.Modified;
           
                //修改仓库数量
                Materialstb m = db.Materialstb.Where(m => m.MAId == MAId).FirstOrDefault();
            if (m.MAmount<=num)
            {
                return 0;
            }
            else
            {
                m.MAmount -= num;
                db.Materialstb.Update(m);

                //添加审核领取表领取时间
                Audit a = new Audit();
                a.AId = AId;
                a.AuditDate = DateTime.Now;
                db.Audit.Add(a);
                return await db.SaveChangesAsync();
            }
               
            
        }

        /// <summary>
        /// 工具审核
        /// </summary>
        /// <param name="ATId"></param>
        /// <param name="AStatic"></param>
        /// <returns></returns>
       [Route("UpdAT")]
        public async Task<ActionResult<int>> UpdAT(int ATId)
        {
           
                AddTool at = db.AddTool.Find(ATId);
                at.AStatic = 1;
                db.Entry(at).State = EntityState.Modified;

            //添加审核领取表领取时间
            AuditTool aut = new AuditTool();
            aut.ATId = ATId;
            aut.AuditToolDate = DateTime.Now;
            db.AuditTool.Add(aut);
           

            return await db.SaveChangesAsync();
            
        }

        #endregion

        #region 申请

            /// <summary>
            /// 材料申请
            /// </summary>
            /// <param name="a"></param>
            /// <returns></returns>   

            [Route("AddAF")]
        public async Task<ActionResult<int>> AddAF([FromBody] ApplyFortb a)
        {
            ApplyFortb af = new ApplyFortb();
            af.MAId = a.MAId;
            af.MaterialAmount = a.MaterialAmount;
            af.UId = a.UId;
            af.AStatic = 0;
            af.AppDate = DateTime.Now;
            db.ApplyFortb.Add(af);
            return await db.SaveChangesAsync();
        }
        /// <summary>
        /// 工具申请
        /// </summary>
        /// <param name="at"></param>
        /// <returns></returns>
        [Route("AddAT")]
        public async Task<ActionResult<int>> AddAT([FromBody] AddTool at)
        {
            AddTool at1 = new AddTool();
            at1.TId = at.TId;
            at1.UId = at.UId;
            at1.AStatic = 0;
            at1.AppDate = DateTime.Now;
            db.AddTool.Add(at1);
            return await db.SaveChangesAsync();
        }

        #endregion

        #region 查询材料信息并进货

        /// <summary>
        /// 查询材料信息并显示
        /// </summary>
        /// <param name="MaterialName"></param>
        /// <param name="CId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("MaterialShow")]
        public FenYe<Ma> MaterialShow(string MaterialName="",int CId=0, int pageIndex = 1, int pageSize = 2)
        {
            if (pageIndex<1)
            {
                pageIndex = 1;
            }
            var list = (from m in db.Materialstb
                        join c in db.Category on m.CategoryId equals c.CId
                        select new Ma
                        {
                            MAId = m.MAId,
                            MaterialName = m.MaterialName,
                            MSpecification = m.MSpecification,
                            CId=c.CId,
                            CName = c.CName,
                            MAmount = m.MAmount,
                            MImg = m.MImg
                        }).ToList();
            if (!string.IsNullOrEmpty(MaterialName))
            {
                list = list.Where(a => a.MaterialName.Contains(MaterialName)).ToList();
            }
            if (CId!=0)
            {
                list = list.Where(c=>c.CId==CId).ToList();
            }

            int count = list.Count();
            var totalpage = count / pageSize + (count % pageSize == 0 ? 0 : 1);
            if (pageIndex > totalpage)
            {
                pageIndex = totalpage;
            }
            FenYe<Ma> fenYeMa = new FenYe<Ma>();
            fenYeMa.Zongtiaoshu = count;
            fenYeMa.Zongyeshu = totalpage;
            fenYeMa.Dangqianye = pageIndex;
            fenYeMa.masd = list.Skip((pageIndex-1) * pageSize).Take(pageSize).ToList();

            return fenYeMa;

        }

        /// <summary>
        /// 根据ID进行反填
        /// </summary>
        /// <param name="MAId"></param>
        /// <returns></returns>
        [Route("GetMAterialById")]
        public async Task<ActionResult<IEnumerable<Ma>>> GetMAterialById(int MAId)
        {
            var list = (from m in db.Materialstb
                        join c in db.Category on m.CategoryId equals c.CId
                        select new Ma
                        {
                            MAId = m.MAId,
                            MaterialName = m.MaterialName,
                            MSpecification = m.MSpecification,
                            CId = c.CId,
                            CName = c.CName,
                            MAmount = m.MAmount,
                            MImg = m.MImg
                        });
            if (MAId!=0)
            {
                list = list.Where(s => s.MAId == MAId);
            }
            return await list.ToListAsync();
        }

        /// <summary>
        /// ，修改材料表数据（材料采购）
        /// </summary>
        /// <param name="MaId"></param>
        /// <param name="MAmount"></param>
        /// <returns></returns>
       [Route("AddMamount")]
        public async Task<ActionResult<int>> AddMamount(int MAId,int MAmount)
        {
            var list = db.Materialstb.Where(m => m.MAId == MAId).FirstOrDefault();
            list.MAmount += MAmount;
            db.Materialstb.Update(list);
            return await db.SaveChangesAsync();
        }

        #endregion
        
        #region 查询工具显示
        [Route("ToolShow")]
        
        public FenYe<TS> ToolShow(string ToolName="", int pageIndex = 1, int pageSize = 2)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            var list = (from t in db.Tooltb
                      
                        select new TS
                        {
                            TId = t.TId,
                            ToolName = t.ToolName,
                            TSpecification=t.TSpecification,
                            Img = t.Img
                        });
            if (!string.IsNullOrEmpty(ToolName))
            {
                list = list.Where(a => a.ToolName.Contains(ToolName));
            }
            int count = list.Count();
            var totalpage = count / pageSize + (count % pageSize == 0 ? 0 : 1);
            if (pageIndex > totalpage)
            {
                pageIndex = totalpage;
            }
            FenYe<TS> fenYeTS = new FenYe<TS>();
            fenYeTS.Zongtiaoshu = count;
            fenYeTS.Zongyeshu = totalpage;
            fenYeTS.Dangqianye = pageIndex;
            fenYeTS.masd = list.Skip((pageIndex-1) * pageSize).Take(pageSize).ToList();

            return fenYeTS;
        }

        #endregion

        #region 材料/工具使用明细
        /// <summary>
        /// 材料使用明细
        /// </summary>
        /// <param name="MAId"></param>
        /// <returns></returns>
        [Route("GetUseMaterial")]
        public async Task<ActionResult<IEnumerable<MXQ>>> GetUseMaterial(int MAId)
        {
            var list = from m in db.Materialstb
                        join af in db.ApplyFortb on m.MAId equals af.MAId
                        join a in db.Audit on af.AId equals a.AId
                        join u in db.UserInfotb on af.UId equals u.UId
                        join md in db.MaintenanceDetailstb on u.UId equals md.UId
                        join ud in db.UserRepairsDetailstb on md.URDId equals ud.UrdId orderby a.AuditDate descending
                        select new MXQ
                        {
                            MAId=m.MAId,
                            UName = u.UName,
                            Marque = ud.Marque,
                            Type = ud.Type,
                            State = ud.State,
                            UPhone = u.UPhone,
                            AuditDate = a.AuditDate,
                            MaterialName = m.MaterialName
                        };
            if (MAId!=0)
            {
                list = list.Where(m => m.MAId == MAId).Take(10);
            }
            return await list.ToListAsync();
        }

        /// <summary>
        /// 工具使用详情
        /// </summary>
        /// <param name="TId"></param>
        /// <returns></returns>
        [Route("GetUseTool")]
        //FenYe<AF> ApplyFortbShow(string MaterialName = "", string UName = "", int pageIndex = 1, int pageSize = 2)
        public async Task<ActionResult<IEnumerable<TXQ>>> GetUseTool(int TId)
        {
            var list = from t in db.Tooltb
                       join at in db.AddTool on t.TId equals at.TId
                       join a in db.AuditTool on at.ATId equals a.ATId
                       join u in db.UserInfotb on at.UId equals u.UId
                       orderby a.AuditToolDate descending 
                       select new TXQ
                       {
                           TId = t.TId,
                           UName = u.UName,
                           UPhone = u.UPhone,
                           AuditToolDate = a.AuditToolDate,
                           ToolName = t.ToolName
                       };
            if (TId !=0)
            {
                list = list.Where(s => s.TId == TId).Take(10);
            }
           
            return await list.ToListAsync();
        }
        #endregion
    }
}