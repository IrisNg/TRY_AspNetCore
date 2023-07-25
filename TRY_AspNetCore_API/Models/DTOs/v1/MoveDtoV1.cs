using TRY_AspNetCore_API.Models.Domain;

namespace TRY_AspNetCore_API.Models.DTOs.v1
{
    public class MoveDtoV1
    {
        public int Id { get; set; }


        public string? Url { get; set; }

        public string Name { get; set; }

        public string? Type { get; set; }
        public int? Power { get; set; }
        public int? Accuracy { get; set; }
        public int? PP { get; set; }
    }
}
