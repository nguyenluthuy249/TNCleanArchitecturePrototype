using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.CleanArchitectureBlazorApp.Application.Framework.Exceptions
{
    public class ApplicationGeneralException : Exception
    {
        public ApplicationGeneralException() { }

        public ApplicationGeneralException(string message) : base(message) { }

        public ApplicationGeneralException(string message, Exception innerException) : base(message, innerException) { }
    }
}
