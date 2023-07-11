using OnlineLearning.Models;

namespace OnlineLearning.Services.AssignmentServices
{
    public interface IAssignmentService
    {
        Task<Assignment> GetByIdAsync(int id);
        Task<Assignment> CreateAsync(Assignment assignment);
        Task<Assignment> UpdateAsync(int id, Assignment assignment);
        Task<bool> DeleteAsync(int id);
    }
}
