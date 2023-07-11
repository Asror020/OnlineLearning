using System.Text.Json.Serialization;

namespace OnlineLearning.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime Deadline { get; set; }
        public byte Grade { get; set; }

        public int InstructorId { get; set; }

        public Instructor Instructor { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set;}
    }
}
