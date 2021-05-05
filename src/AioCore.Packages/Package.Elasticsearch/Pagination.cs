using System.Collections.Generic;

namespace Package.Elasticsearch
{
    public interface IPagination
    {
        IReadOnlyCollection<object> Items { get; }
        int Page { get; set; }
        int PageSize { get; set; }
        long TotalRecords { get; set; }
        long TotalPage { get; }
        bool HasNextPage { get; }
        public bool HasPreviousPage { get; }
    }

    public class Pagination<T> : IPagination
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public long TotalRecords { get; set; }

        public long TotalPage => ((TotalRecords - 1) / PageSize) + 1;

        public bool HasNextPage => Page < TotalPage;

        public bool HasPreviousPage => Page > 1;

        public IReadOnlyCollection<T> Items { get; set; }

        IReadOnlyCollection<object> IPagination.Items => (IReadOnlyCollection<object>)Items;

        public Pagination(int page, int pageSize, List<T> items, long total)
        {
            Page = page;
            PageSize = pageSize;
            Items = items;
            TotalRecords = total;
        }
    }
}