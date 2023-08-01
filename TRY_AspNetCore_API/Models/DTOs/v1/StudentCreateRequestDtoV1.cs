using System.ComponentModel.DataAnnotations;

namespace TRY_AspNetCore_API.Models.DTOs.v1
{
    public class StudentCreateRequestDtoV1
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public List<CourseDtoV1> Courses { get; set; }
    }
}
