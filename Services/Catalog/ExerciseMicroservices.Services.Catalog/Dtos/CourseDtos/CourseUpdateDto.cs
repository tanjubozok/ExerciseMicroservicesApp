using ExerciseMicroservices.Services.Catalog.Dtos.FeatureDtos;

namespace ExerciseMicroservices.Services.Catalog.Dtos.CourseDtos
{
    public class CourseUpdateDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string AppUserId { get; set; }
        public FeatureDto Feature { get; set; }
        public string CategoryId { get; set; }
    }
}
