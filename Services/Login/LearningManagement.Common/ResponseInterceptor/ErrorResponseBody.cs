using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Common.ResponseInterceptor
{
    public class ErrorResponseBody
    {
        public Error Error { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public ErrorResponseBody(string message, int errorcode)
        {
            this.Error = new Error();
            this.Error.Reason = errorcode switch
            {
                StatusCodes.Status401Unauthorized => "Bad Request",
                StatusCodes.Status403Forbidden => "Forbidden",
            };
            this.Error.Code = errorcode;
            this.Error.Description = message;
             
        }
    }
}
