using OnlineLearning.Models;

namespace OnlineLearning.Services.StudentServices
{
    public interface IStudentService
    {
        Task<Student> GetByIdAsync(int id);
        Task<Student> CreateAsync(Student student);
        Task<Student> UpdateAsync(int id, Student student);
        Task<bool> DeleteAsync(int id);
    }
}
