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

       
    }
}