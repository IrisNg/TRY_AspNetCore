using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TRY_AspNetCore_API.ActionFilters;
using TRY_AspNetCore_API.Data;
using TRY_AspNetCore_API.Logging;
using TRY_AspNetCore_API.Models.Domain;
using TRY_AspNetCore_API.Models.DTOs.v1;
using TRY_AspNetCore_API.Repositories;

namespace TRY_AspNetCore_API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class StudentsController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<StudentsController> _logger;
        private readonly IMapper _mapper;
        private readonly ILogging _logFormatter;

        public StudentsController(
            ApplicationDbContext dbContext,
            ILogger<StudentsController> logger,
            IMapper mapper,
            ILogging logFormatter
            )
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            _logFormatter = logFormatter;
        }


        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> CreateStudentV1([FromBody] StudentCreateRequestDtoV1 requestDto)
        {
            var coursesDomainModel = new List<Course>();
            foreach (var courseDtoV1 in requestDto.Courses)
            {
                var courseDomainModel = new Course
                {
                    Name = courseDtoV1.Name
                };

                await _dbContext.Courses.AddAsync(courseDomainModel);
                await _dbContext.SaveChangesAsync();

                // Course entity has gotten Id here
                coursesDomainModel.Add(courseDomainModel);
            }

            var studentDomainModel = new Student { Name = requestDto.Name, Courses = coursesDomainModel };

            await _dbContext.Students.AddAsync(studentDomainModel);
            await _dbContext.SaveChangesAsync();


            return Ok();
        }

    }
}
