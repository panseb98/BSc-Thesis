using System;
using System.Collections.Generic;
using System.Text;

namespace Authorization.Helpers.Exceptions
{
    public class UserExistsException : Exception
    {
        public UserExistsException(string message) : base(message)
        {

        }
    }
}
