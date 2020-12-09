﻿namespace Common.Models
{
    public class PaginatedResult<T>
    {
        public string Next { get; set; }
        public string Previous{ get; set; }
        public int Total { get; set; }
        public int Current { get; set; }
        public T Data { get; set; }
    }
}
