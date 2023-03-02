using ExerciseMicroservices.Services.Catalog.Dtos.CategoryDtos;
using ExerciseMicroservices.Services.Catalog.Dtos.FeatureDtos;
using System;

namespace ExerciseMicroservices.Services.Catalog.Dtos.CourseDtos
{
    public class CourseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public DateTime CreatedTime { get; set; }
        public string AppUserId { get; set; }
        public FeatureDto Feature { get; set; }
        public string CategoryId { get; set; }
        public CategoryDto Category { get; set; }
    }
}
