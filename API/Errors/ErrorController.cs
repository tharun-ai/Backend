using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Errors
{   
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi=true)]
    public class ErrorController
    {
        public IActionResult Error(int code){
            return new ObjectResult(new ErrorReponse(code));
        }
    }
}