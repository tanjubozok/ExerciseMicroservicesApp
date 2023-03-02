using AutoMapper;
using ExerciseMicroservices.Services.Catalog.Dtos.CategoryDtos;
using ExerciseMicroservices.Services.Catalog.Dtos.CourseDtos;
using ExerciseMicroservices.Services.Catalog.Dtos.FeatureDtos;
using ExerciseMicroservices.Services.Catalog.Models;

namespace ExerciseMicroservices.Services.Catalog.Mappings
{
    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CourseCreateDto>().ReverseMap();
            CreateMap<Course, CourseUpdateDto>().ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Feature, FeatureDto>().ReverseMap();
        }
    }
}
