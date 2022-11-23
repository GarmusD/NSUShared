using System;
using System.Collections.Generic;
using System.Text;

namespace NSU.Shared.NSUNet
{
    public class EmptyHostException : System.Exception
    {
        public EmptyHostException() : base("The Host value cannot be empty.")
        {
        }

        public EmptyHostException(string message) : base(message)
        {
        }
    }
}
