using OnlineLearning.Data;
using OnlineLearning.Models.Exceptions;
using OnlineLearning.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineLearning.Services.AssignmentServices
{
    public class AssignmentService : IAssignmentService
    {
        private readonly ApplicationDbContext context;

        public AssignmentService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Assignment> GetByIdAsync(int id)
        {
            ValidateCourseId(id);

            var maybeAssignment = await this.context.Assignments.SingleOrDefaultAsync(c => c.Id == id);
            if (maybeAssignment == null)
                throw new EntryPointNotFoundException($"Assignment with this {nameof(id)} is not found.");

            return maybeAssignment;
        }

        public async Task<Assignment> CreateAsync(Assignment assignment)
        {
            ValidateCourse(assignment);

            this.context.Assignments.Add(assignment);
            await this.context.SaveChangesAsync();

            return assignment;
        }

        public async Task<Assignment> UpdateAsync(int id, Assignment assignment)
        {
            ValidateCourseId(id);
            ValidateCourse(assignment);

            var assignmentFound = await GetByIdAsync(id);

            assignmentFound.Title = assignment.Title;
            assignmentFound.Description = assignment.Description;
            assignmentFound.Status = assignment.Status;

            this.context.Update(assignmentFound);
            await this.context.SaveChangesAsync();

            return assignmentFound;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            ValidateCourseId(id);

            var assignmentFound = await GetByIdAsync(id);

            var result = this.context.Assignments.Remove(assignmentFound);
            await this.context.SaveChangesAsync();

            return result != null;
        }

        private static void ValidateCourse(Assignment assignment)
        {
            if (assignment == null) throw new BadRequestException($"{nameof(assignment)} is null.");

            var badrequestExceptionMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(assignment.Title))
                badrequestExceptionMessage += nameof(assignment.Title) + " is required. ";

            if (string.IsNullOrWhiteSpace(assignment.Description))
                badrequestExceptionMessage += nameof(assignment.Description) + " is required. ";

            if (string.IsNullOrWhiteSpace(assignment.Status))
                badrequestExceptionMessage += nameof(assignment.Status) + " is required. ";

            if (!string.IsNullOrWhiteSpace(badrequestExceptionMessage))
                throw new BadRequestException(badrequestExceptionMessage);
        }

        private static void ValidateCourseId(int id)
        {
            if (id < 1) throw new BadRequestException("Id is invalid.");
        }
    }
}
