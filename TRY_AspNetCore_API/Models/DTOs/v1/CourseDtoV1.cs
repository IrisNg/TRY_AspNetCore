using System.ComponentModel.DataAnnotations;

namespace TRY_AspNetCore_API.Models.DTOs.v1
{
    public class CourseDtoV1
    {
        [Required]
        public string Name { get; set; }
    }
}
