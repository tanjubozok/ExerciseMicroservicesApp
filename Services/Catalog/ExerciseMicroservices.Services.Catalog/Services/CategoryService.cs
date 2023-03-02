using AutoMapper;
using ExerciseMicroservices.Services.Catalog.Dtos.CategoryDtos;
using ExerciseMicroservices.Services.Catalog.Models;
using ExerciseMicroservices.Services.Catalog.Settings;
using ExerciseMicroservices.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExerciseMicroservices.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(x => true).ToListAsync();
            var categoryDto = _mapper.Map<List<CategoryDto>>(categories);
            return ResponseDto<List<CategoryDto>>.Success(categoryDto, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<CategoryDto>> CreateAsync(Category category)
        {
            await _categoryCollection.InsertOneAsync(category);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return ResponseDto<CategoryDto>.Success(categoryDto, StatusCodes.Status201Created);
        }

        public async Task<ResponseDto<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (category is null)
                return ResponseDto<CategoryDto>.Fail("Kategori bulunamadı", StatusCodes.Status404NotFound);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return ResponseDto<CategoryDto>.Success(categoryDto, StatusCodes.Status200OK);
        }
    }
}
