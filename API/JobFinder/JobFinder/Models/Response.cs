using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Models
{
    public class Response<T>
    {
        public Response()
        {
        }

        public Response(Exception exception)
        {
            Succeeded = false;
            Message = exception.Message;
            Errors = new string[] { exception.Message };
        }

        public Response(Either<Exception, T> eitherDatas)
        {
            eitherDatas
                .IfLeft(ex =>
                {
                    Succeeded = false;
                    Message = ex.Message;
                    Errors = new string[] { ex.Message };
                });

            eitherDatas
                .IfRight(data =>
                {
                    Succeeded = true;
                    Message = string.Empty;
                    Errors = null;
                    Data = data;
                });
        }

        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        public Response(string message, bool succeeded)
        {
            Succeeded = succeeded;
            Message = message;
            Errors = null;
        }

        public Response(string message, string[] errors)
        {
            Succeeded = false;
            Message = message;
            Errors = errors;
        }

        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
