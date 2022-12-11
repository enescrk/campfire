using System;
using System.Collections.Generic;

namespace HilfeOhneGrenzen.Domain.SeedWork.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException(string message) : base(message)
        {

        }

        public AuthException(List<string> messages)
        : base(string.Join(" | ", messages))
        {

        }
    }
}