using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Application.Common.Models
{
    public class PagingQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortField { get; set; }

        /// <summary>
        /// Ascending or Descending
        /// </summary>
        /// <example>Asc/Desc</example>
        public string SortOrder { get; set; }
    }
}
