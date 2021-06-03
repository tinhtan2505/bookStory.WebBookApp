using System;
using System.Collections.Generic;
using System.Text;

namespace bookStory.Utilities.Exceptions
{
    public class BookException : Exception
    {
        public BookException()
        {
        }

        public BookException(string message)
            : base(message)
        {
        }

        public BookException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}