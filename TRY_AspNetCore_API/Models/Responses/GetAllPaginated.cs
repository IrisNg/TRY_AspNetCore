namespace TRY_AspNetCore_API.Models.Responses
{
    public class GetAllPaginated<TItems>
    {
        public TItems Items { get; set; }
        public int TotalItemsCount { get; set; }

        public int PageNumber { get; set; }
    }
}
