using AutoMapper;
using ExerciseMicroservices.Services.Catalog.Dtos.CourseDtos;
using ExerciseMicroservices.Services.Catalog.Models;
using ExerciseMicroservices.Services.Catalog.Settings;
using ExerciseMicroservices.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExerciseMicroservices.Services.Catalog.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(x => true).ToListAsync();
            if (courses.Any())
                foreach (var item in courses)
                    item.Category = await _categoryCollection.Find(x => x.Id == item.CategoryId).FirstAsync();
            else
                courses = new List<Course>();

            var courseDto = _mapper.Map<List<CourseDto>>(courses);
            return ResponseDto<List<CourseDto>>.Success(courseDto, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (course is null)
                return ResponseDto<CourseDto>.Fail("Kurs bulunamadı", StatusCodes.Status404NotFound);
            course.Category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstAsync();
            var courseDto = _mapper.Map<CourseDto>(course);
            return ResponseDto<CourseDto>.Success(courseDto, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(x => x.AppUserId == userId).ToListAsync();
            if (courses.Any())
                foreach (var item in courses)
                    item.Category = await _categoryCollection.Find(x => x.Id == item.CategoryId).FirstAsync();
            else
                courses = new List<Course>();
            var courseDto = _mapper.Map<List<CourseDto>>(courses);
            return ResponseDto<List<CourseDto>>.Success(courseDto, StatusCodes.Status200OK);
        }

        public async Task<ResponseDto<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
        {
            var course = _mapper.Map<Course>(courseCreateDto);
            course.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(course);
            var courseDto = _mapper.Map<CourseDto>(course);
            return ResponseDto<CourseDto>.Success(courseDto, StatusCodes.Status201Created);
        }

        public async Task<ResponseDto<NoContentDto>> UpdateAsync(CourseUpdateDto courseUpdateDto)
        {
            var course = _mapper.Map<Course>(courseUpdateDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, course);
            if (result is null)
                return ResponseDto<NoContentDto>.Fail("Hata oluştu", StatusCodes.Status400BadRequest);
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<ResponseDto<NoContentDto>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(id);
            if (result is null)
                return ResponseDto<NoContentDto>.Fail("Hata oluştu", StatusCodes.Status400BadRequest);
            return ResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}
