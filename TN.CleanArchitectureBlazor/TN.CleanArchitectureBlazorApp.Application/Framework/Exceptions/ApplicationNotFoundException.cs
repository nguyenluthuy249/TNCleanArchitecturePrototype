using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.CleanArchitectureBlazorApp.Application.Framework.Exceptions
{
    public class ApplicationNotFoundException : Exception
    {
        public ApplicationNotFoundException() { }
        public ApplicationNotFoundException(string message) : base(message) { }
    }
}
