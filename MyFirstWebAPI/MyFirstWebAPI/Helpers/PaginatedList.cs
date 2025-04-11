namespace MyFirstWebAPI.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; set; } // Trang hiện tại
        public int TotalPages { get; set; } // Tổng số trang

        public PaginatedList(List<T> items, int itemsLength, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(itemsLength / (double)pageSize); // tính tổng số trang

            this.AddRange(items);
        }

        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            int itemsLength = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, itemsLength, pageIndex, pageSize);
        }
    }
}
