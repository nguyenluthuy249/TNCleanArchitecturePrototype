using TN.Prototype.CleanArchitecture.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Application.Common.Interfaces
{
    public interface IHasApplicationEvent
    {
        List<ApplicationEvent> ApplicationEvents { get; set; }
    }
}
