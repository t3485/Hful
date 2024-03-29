﻿namespace Hful.Core.Application
{
    public class PageDto<T>
    {
        public PageDto()
        {
            Rows = new List<T>();
        }

        public PageDto(IEnumerable<T> rows)
        {
            Rows = rows.ToList();
            Total = Rows.Count;
        }

        public PageDto(List<T> rows, int total, int page)
        {
            Rows = rows;
            Total = total;
            Page = page;
        }

        public List<T> Rows { get; set; }

        public int Total { get; set; }

        public int Page { get; set; }
    }
}
