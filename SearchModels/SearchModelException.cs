using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchModels
{
    public class SearchModelException : Exception
    {
        public SearchModelException(string? message) : base(message)
        {
        }

        public SearchModelException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
