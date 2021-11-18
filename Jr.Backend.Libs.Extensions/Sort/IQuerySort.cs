namespace Jr.Backend.Libs.Extensions.Sort
{
    public interface IQuerySort : ICustomQueryable
    {
        string Sort { get; set; }
    }
}