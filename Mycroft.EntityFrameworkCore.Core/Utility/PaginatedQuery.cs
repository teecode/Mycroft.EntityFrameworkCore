using System;
using System.Linq.Expressions;
using static Mycroft.EntityFrameworkCore.Core.Models.Enumerators;

namespace Mycroft.EntityFrameworkCore.Core.Utility
{
    public class PaginatedQuery<TEntity>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public Order Order { get; set; } = Order.Ascending;
        public Expression<Func<TEntity, bool>> predicate;
        public string[] ChildObjectNamesToInclude { get; set; } 
    }
}