using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Models;
using OnlineLearning.Models.Exceptions;
using OnlineLearning.Services.ProfessorServices;
using System.Text.RegularExpressions;

namespace OnlineLearning.Services.InstructorServices
{
    public class InstructorService : IInstructorService
    {
        private readonly ApplicationDbContext context;

        public InstructorService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Instructor> GetByIdAsync(int id)
        {
            ValidateInstructorId(id);

            var maybeInstructor = await this.context.Instructors.FirstOrDefaultAsync(x => x.Id == id);

            if(maybeInstructor == null) 
                throw new EntryPointNotFoundException($"Instructor with this {id} is not found. ");

            return maybeInstructor;
        }

        public async Task<Instructor> CreateAsync(Instructor instructor)
        {
            ValidateInstructor(instructor);

            this.context.Instructors.Add(instructor);
            await this.context.SaveChangesAsync();

            return instructor;
        }

        public async Task<Instructor> UpdateAsync(int id, Instructor instructor)
        {
            ValidateInstructorId(id);
            ValidateInstructor(instructor);

            var instructorFound = await GetByIdAsync(id);
            
            instructorFound.FirstName = instructor.FirstName;
            instructorFound.LastName = instructor.LastName;
            instructorFound.PhoneNumber = instructor.PhoneNumber;
            instructorFound.EmailAddress = instructor.EmailAddress;

            this.context.Update(instructorFound);
            await this.context.SaveChangesAsync();

            return instructorFound;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            ValidateInstructorId(id);

            var maybeInstructor = await this.GetByIdAsync(id);

            var result = this.context.Remove(maybeInstructor);
            await this.context.SaveChangesAsync();

            return result != null;
        }

        private static void ValidateInstructor(Instructor instructor)
        {
            if (instructor == null) throw new BadRequestException($"{nameof(instructor)} is null.");

            var badrequestExceptionMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(instructor.FirstName))
                badrequestExceptionMessage += nameof(instructor.FirstName) + " is required. ";

            if (string.IsNullOrWhiteSpace(instructor.LastName))
                badrequestExceptionMessage += nameof(instructor.FirstName) + " is required. ";

            if (string.IsNullOrWhiteSpace(instructor.PhoneNumber))
                badrequestExceptionMessage += nameof(instructor.PhoneNumber) + " is required. ";
            else if (!Regex.IsMatch(instructor.PhoneNumber, @"^\d{10}$"))
                badrequestExceptionMessage += "Phone number should contain 10 digits only. ";

            if (string.IsNullOrWhiteSpace(instructor.EmailAddress))
                badrequestExceptionMessage += nameof(instructor.EmailAddress) + " is required. ";
            else if (!Regex.IsMatch(instructor.EmailAddress, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                badrequestExceptionMessage += ("Email is invalid. ");

            if (string.IsNullOrWhiteSpace(instructor.Password))
                badrequestExceptionMessage += nameof(instructor.Password) + " is required.";
            else if (!Regex.IsMatch(instructor.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"))
                badrequestExceptionMessage += "Password should have at least 8 length, one uppercase, one lowercase and one digit.";

            if (!string.IsNullOrWhiteSpace(badrequestExceptionMessage))
                throw new BadRequestException(badrequestExceptionMessage);
        }

        private static void ValidateInstructorId(int id)
        {
            if (id < 1) throw new BadRequestException("Id is invalid.");
        }
    }
}
