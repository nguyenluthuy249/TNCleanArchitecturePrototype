using TN.Prototype.CleanArchitecture.Application.Common.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Application.Common.Models
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; }
        public int PageNumber { get; }
        public int PageCount { get; }
        public int Count { get; }

        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageCount = (int)Math.Ceiling(count / (double)pageSize);
            Count = count;
            Items = items;
        }

        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < PageCount;
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize, string sortField, string sortOrder)
        {
            var count = await source.CountAsync();
            var items = new List<T>();
            var propertyInfo = sortField != null ? typeof(T).GetProperty(sortField) : null;
            if (propertyInfo == null)
            {
                items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {

                items = sortOrder.ToLower() == SortOrder.Desc ? await source.OrderBy($"{sortField} {SortOrder.Desc}").Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync()
                                                              : await source.OrderBy($"{sortField} {SortOrder.Asc}").Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            }
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
