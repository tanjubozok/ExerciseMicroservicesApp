using ExerciseMicroservices.Services.Catalog.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseMicroservices.Services.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
    }
}
