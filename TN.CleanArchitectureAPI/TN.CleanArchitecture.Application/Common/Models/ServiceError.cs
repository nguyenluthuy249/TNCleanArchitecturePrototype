using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Application.Common.Models
{
    [Serializable]
    public class ServiceError
    {
        public string Message { get; }

        /// <summary>
        /// Machine readable error code
        /// </summary>
        public int Code { get; }

        public ServiceError() { }
        public ServiceError(string message, int code)
        {
            Message = message;
            Code = code;
        }

        public static ServiceError Default => new ServiceError("An exception occured.", 500);
        public static ServiceError Authentiation => new ServiceError("Authentication Error", 401);
        public static ServiceError NotFound => new ServiceError("Not found.", 404);
        public static ServiceError Validation => new ServiceError("Validation Error", 400);
        public static ServiceError ValidationFormat => new ServiceError("Request object format is not valid.", 400);
    }
}
