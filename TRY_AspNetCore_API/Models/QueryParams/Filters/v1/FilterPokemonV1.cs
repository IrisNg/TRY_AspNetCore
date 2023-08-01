namespace TRY_AspNetCore_API.Models.QueryParams.Filters.v1
{
    public class FilterPokemonV1
    {
        public DateTime? StartCreatedDate { get; set; }
        public DateTime? EndCreatedDate { get; set; }

        public string? Search { get; set; }
    }
}
