using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Middlewares
{
    public class APIException : System.Exception
    {
        public int StatusCode { get; set; }

        public APIException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;

        }

        public APIException()
        {

        }
    }
}
