using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Models;
using OnlineLearning.Models.Exceptions;
using System.Net;
using System.Text.RegularExpressions;

namespace OnlineLearning.Services.StudentServices
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext context;

        public StudentService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            ValidateStudentId(id);

            var maybeStudent = await this.context.Students.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (maybeStudent == null) 
                throw new EntryPointNotFoundException($"Student with this {nameof(id)} is not found.");

            return maybeStudent;
        }
        public async Task<Student> CreateAsync(Student student)
        {
            ValidateStudent(student);

            this.context.Students.Add(student);
            await this.context.SaveChangesAsync();

            return student;
        }
        public async Task<Student> UpdateAsync(int id, Student student)
        {
            ValidateStudent(student);

            var maybeStudent = await this.GetByIdAsync(id);

            maybeStudent.FirstName = student.FirstName;
            maybeStudent.LastName = student.LastName;
            maybeStudent.PhoneNumber = student.PhoneNumber;
            maybeStudent.EmailAddress = student.EmailAddress;

            this.context.Update(maybeStudent);
            await this.context.SaveChangesAsync();

            return maybeStudent;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            ValidateStudentId(id);

            var maybeStudent = await this.GetByIdAsync(id);

            var result = this.context.Remove(maybeStudent);
            await this.context.SaveChangesAsync();

            return result != null;
        }

        private static void ValidateStudent(Student student)
        {
            if (student == null) throw new BadRequestException($"{nameof(student)} is null.");

            var badrequestExceptionMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(student.FirstName))
                badrequestExceptionMessage += nameof(student.FirstName) + " is required. ";

            if(string.IsNullOrWhiteSpace(student.LastName))
                badrequestExceptionMessage += nameof(student.FirstName) + " is required. ";

            if (string.IsNullOrWhiteSpace(student.PhoneNumber))
                badrequestExceptionMessage += nameof(student.PhoneNumber) + " is required. ";
            else if (!Regex.IsMatch(student.PhoneNumber, @"^\d{10}$"))
                badrequestExceptionMessage += "Phone number should contain 10 digits only. ";

            if (string.IsNullOrWhiteSpace(student.EmailAddress))
                badrequestExceptionMessage += nameof(student.EmailAddress) + " is required. ";
            else if (!Regex.IsMatch(student.EmailAddress, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                badrequestExceptionMessage += ("Email is invalid. ");

            if (string.IsNullOrWhiteSpace(student.Password))
                    badrequestExceptionMessage += nameof(student.Password) + " is required.";
            else if (!Regex.IsMatch(student.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"))
                badrequestExceptionMessage += "Password should have at least 8 length, one uppercase, one lowercase and one digit.";

            if (!string.IsNullOrWhiteSpace(badrequestExceptionMessage))
                throw new BadRequestException(badrequestExceptionMessage);
        }

        private static void ValidateStudentId(int id)
        {
            if (id < 1) throw new BadRequestException("Id is invalid.");
        }
    }
}
