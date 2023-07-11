using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Models;
using OnlineLearning.Models.Exceptions;
using System.Text.RegularExpressions;

namespace OnlineLearning.Services.CourseServices
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext context;

        public CourseService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            ValidateCourseId(id);

            var maybeCourse = await this.context.Courses.SingleOrDefaultAsync(c => c.Id == id);
            if (maybeCourse == null) 
                throw new EntryPointNotFoundException($"Course with this {nameof(id)} is not found.");

            return maybeCourse;
        }

        public async Task<Course> CreateAsync(Course course)
        {
            ValidateCourse(course);

            this.context.Courses.Add(course);
            await this.context.SaveChangesAsync();

            return course;
        }

        public async Task<Course> UpdateAsync(int id, Course course)
        {
            ValidateCourseId(id);
            ValidateCourse(course);

            var courseFound = await GetByIdAsync(id);

            courseFound.Name = course.Name;
            courseFound.Description = course.Description;
            courseFound.Level = course.Level;

            this.context.Update(courseFound);
            await this.context.SaveChangesAsync();

            return courseFound;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            ValidateCourseId(id);

            var courseFound = await GetByIdAsync(id);

            var result = this.context.Courses.Remove(courseFound);
            await this.context.SaveChangesAsync();

            return result != null;
        }

        private static void ValidateCourse(Course course)
        {
            if (course == null) throw new BadRequestException($"{nameof(course)} is null.");

            var badrequestExceptionMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(course.Name))
                badrequestExceptionMessage += nameof(course.Name) + " is required. ";

            if (string.IsNullOrWhiteSpace(course.Description))
                badrequestExceptionMessage += nameof(course.Description) + " is required. ";

            if (string.IsNullOrWhiteSpace(course.Level))
                badrequestExceptionMessage += nameof(course.Level) + " is required. ";

            if (!string.IsNullOrWhiteSpace(badrequestExceptionMessage))
                throw new BadRequestException(badrequestExceptionMessage);
        }

        private static void ValidateCourseId(int id)
        {
            if (id < 1) throw new BadRequestException("Id is invalid.");
        }
    }
}
