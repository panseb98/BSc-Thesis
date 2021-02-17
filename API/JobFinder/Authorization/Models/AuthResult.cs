using System;
using System.Collections.Generic;
using System.Text;

namespace Authorization.Models
{
    public class AuthResult
    {
        public AuthResult()
        {
            Succeed = true;
            Errors = null;
        }

        public AuthResult(string[] errors)
        {
            Succeed = false;
            Errors = errors;
        }
        public bool Succeed { get; set; }
        public string[] Errors { get; set; }
    }
    public class AuthResult<T>
    {
        public AuthResult(string[] errors)
        {
            Succeed = false;
            Errors = errors;
        }
        public AuthResult(T data)
        {
            Succeed = true;
            Result = data;
            Errors = null;
        }
        public bool Succeed { get; set; }
        public T Result { get; set; }
        public string[] Errors { get; set; }
    }
}
