namespace Jr.Backend.Libs.Extensions.Pagination
{
    public interface IQueryPaging : ICustomQueryable
    {
        int? Limit { get; set; }
        int? Offset { get; set; }
    }
}