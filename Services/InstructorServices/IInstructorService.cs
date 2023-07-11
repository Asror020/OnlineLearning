using OnlineLearning.Models;

namespace OnlineLearning.Services.ProfessorServices
{
    public interface IInstructorService
    {
        Task<Instructor>  GetByIdAsync(int id);
        Task<Instructor> CreateAsync(Instructor instructor);
        Task<Instructor> UpdateAsync(int id, Instructor instructor);
        Task<bool> DeleteAsync(int id);
    }
}
