namespace Common.Models
{
    public class PaginatedResult<T>
    {
        public string Next { get; set; }
        public string Previous{ get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int Current { get; set; }
        public T Data { get; set; }
    }
}
