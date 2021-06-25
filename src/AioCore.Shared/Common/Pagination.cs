using System.Collections.Generic;

namespace AioCore.Shared.Common
{
    public class Pagination<T>
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public long TotalRecords { get; set; }

        public long TotalPage => (TotalRecords - 1) / PageSize + 1;

        public bool HasNextPage => Page < TotalPage;

        public bool HasPreviousPage => Page > 1;

        public IReadOnlyCollection<T> Items { get; set; }

        public Pagination(int page, int pageSize, List<T> items, long total)
        {
            Page = page;
            PageSize = pageSize;
            Items = items;
            TotalRecords = total;
        }
    }
}