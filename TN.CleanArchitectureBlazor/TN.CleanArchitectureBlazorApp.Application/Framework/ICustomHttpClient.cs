using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TN.CleanArchitectureBlazorApp.Application.Models;

namespace TN.CleanArchitectureBlazorApp.Application.Framework
{
    public interface ICustomHttpClient
    {
        Task<ApiResult<T>> Get<T>(string path);
        Task<ApiResult<T>> Post<T>(string path, object body);
        Task<ApiResult<T>> Put<T>(string path, object body);
        Task<ApiResult<T>> Delete<T>(string path);
    }
}
