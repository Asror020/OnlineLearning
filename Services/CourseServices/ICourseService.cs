using OnlineLearning.Models;

namespace OnlineLearning.Services.CourseServices
{
    public interface ICourseService
    {
        Task<Course> GetByIdAsync(int id);
        Task<Course> CreateAsync(Course course);
        Task<Course> UpdateAsync(int id, Course course);
        Task<bool> DeleteAsync(int id);
    }
}
