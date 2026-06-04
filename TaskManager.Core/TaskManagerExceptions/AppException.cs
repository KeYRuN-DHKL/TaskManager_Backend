using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TaskManager.Core.TaskManagerExceptions
{
    public abstract class AppException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected AppException(string message,HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
