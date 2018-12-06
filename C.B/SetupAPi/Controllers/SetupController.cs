using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using C.B.Common.helper;

namespace SetupAPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        // GET api/values
        [HttpGet("publish")]
        public ActionResult<string> Get()
        {
            var filePath = Environment.CurrentDirectory;
            var fileName = "publish.bat";
            CmdTool.RunBat(filePath,fileName);
            return "Seccess";
        }

        
    }
}
