using TN.Prototype.CleanArchitecture.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Application.Common.Extensions
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, PagingQuery pagingQuery)
            => PaginatedList<TDestination>.CreateAsync(queryable, pagingQuery.PageNumber, pagingQuery.PageSize, pagingQuery.SortField, pagingQuery.SortOrder);
    }
}
