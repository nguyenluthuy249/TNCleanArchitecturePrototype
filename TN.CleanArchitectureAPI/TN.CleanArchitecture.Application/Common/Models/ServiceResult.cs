using TN.Prototype.CleanArchitecture.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Application.Common.Models
{
    public class ServiceResult
    {
        public string ApiVersion { get; set; }
        public DateTime RequestDate => DateTime.Now;
        public bool Succeeded => this.Error == null;
        public ServiceError Error { get; set; }
        public ServiceResult() { }
        public ServiceResult(ServiceError error)
        {
            if (error == null)
            {
                error = ServiceError.Default;
            }

            Error = error;
        }

        public static ServiceResult Failed(ServiceError error)
        {
            return new ServiceResult(error);
        }

        public static ServiceResult<T> Failed<T>(ServiceError error)
        {
            return new ServiceResult<T>(error);
        }

        public static ServiceResult<T> Failed<T>(T data, ServiceError error)
        {
            return new ServiceResult<T>(data, error);
        }

        public static ServiceResult<T> Success<T>(T data)
        {
            return new ServiceResult<T>(data);
        }
    }

    /// <summary>
    /// A standard response for service calls.
    /// </summary>
    /// <typeparam name="T">Return data type</typeparam>
    public class ServiceResult<T> : ServiceResult, IHasApplicationEvent
    {
        public T Data { get; set; }
        
        [JsonIgnore]
        public List<ApplicationEvent> ApplicationEvents { get; set; }

        public ServiceResult(T data)
        {
            Data = data;
            ApplicationEvents = new List<ApplicationEvent>();
        }

        public ServiceResult(T data, ServiceError error) : base(error)
        {
            Data = data;
        }

        public ServiceResult(ServiceError error) : base(error) { }
    }
}
