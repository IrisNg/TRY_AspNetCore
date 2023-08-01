namespace TRY_AspNetCore_API.Repositories
{
    public class WithCount<TData>
    {
        public required TData Data { get; set; }
        public required int Count { get; set; }
    }
}
