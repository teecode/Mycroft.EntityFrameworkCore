using System;
using System.Collections.Generic;
using System.Text;

namespace Mycroft.EntityFrameworkCore.Core.Utility
{
    public class Paginated<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
        public IEnumerable<T> Data { get; set; }
        public int TotalPages => (int)Math.Ceiling(this.TotalCount / (double)this.PageSize);
    }
}
