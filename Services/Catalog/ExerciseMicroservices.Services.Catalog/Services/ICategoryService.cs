using ExerciseMicroservices.Services.Catalog.Dtos.CategoryDtos;
using ExerciseMicroservices.Services.Catalog.Models;
using ExerciseMicroservices.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExerciseMicroservices.Services.Catalog.Services
{
    public interface ICategoryService
    {
        Task<ResponseDto<CategoryDto>> CreateAsync(Category category);
        Task<ResponseDto<List<CategoryDto>>> GetAllAsync();
        Task<ResponseDto<CategoryDto>> GetByIdAsync(string id);
    }
}
