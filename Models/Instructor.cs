namespace OnlineLearning.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;

        public List<Course> Courses { get; } = new();
    }
}
