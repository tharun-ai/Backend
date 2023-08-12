using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Errors
{
    public class ErrorReponse
    {
       
         public ErrorReponse(int statusCode,string message=null)
        {
            StatusCode=statusCode;
            Message=message?? GetDefaultMessageforStatusCode(statusCode);
        }

        private string GetDefaultMessageforStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400=>"A bad request,you have made",
                
                401=>"Authorized, you are not",
                404=>"Resource found,it was not",
                500=>"An unuseful error for you",
                _ => null
            };
        }

        public int StatusCode{get;set;}

        public string Message{get;set;}

       



        
    }
}