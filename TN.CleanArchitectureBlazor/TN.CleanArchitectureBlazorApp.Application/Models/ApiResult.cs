using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.CleanArchitectureBlazorApp.Application.Models
{
    public class ApiResult
    {
        public DateTime RequestDate => DateTime.Now;
        public bool Succeeded => Error == null;
        public ApiError Error { get; set; }
    }

    public class ApiResult<T> : ApiResult
    {
        public T Data { get; set; }
    }
}
