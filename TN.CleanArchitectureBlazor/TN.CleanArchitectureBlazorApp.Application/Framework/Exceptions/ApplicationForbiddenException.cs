using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.CleanArchitectureBlazorApp.Application.Framework.Exceptions
{
    public class ApplicationForbiddenException : Exception
    {
        public ApplicationForbiddenException() { }
        public ApplicationForbiddenException(string message) : base(message) { }
    }
}
