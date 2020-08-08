using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ItemShop.Controllers
{
    
    public class HomeController : ApiController
    {
        public ActionResult<string> Get()
        {
            return Ok("hello");
        }
    }
}
