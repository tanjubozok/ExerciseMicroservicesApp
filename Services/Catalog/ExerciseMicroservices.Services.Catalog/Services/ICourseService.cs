using ExerciseMicroservices.Services.Catalog.Dtos.CourseDtos;
using ExerciseMicroservices.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExerciseMicroservices.Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<ResponseDto<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        Task<ResponseDto<NoContentDto>> DeleteAsync(string id);
        Task<ResponseDto<List<CourseDto>>> GetAllAsync();
        Task<ResponseDto<List<CourseDto>>> GetAllByUserIdAsync(string userId);
        Task<ResponseDto<CourseDto>> GetByIdAsync(string id);
        Task<ResponseDto<NoContentDto>> UpdateAsync(CourseUpdateDto courseUpdateDto);
    }
}
