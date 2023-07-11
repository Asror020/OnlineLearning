namespace OnlineLearning.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Level { get; set; } = null!;

        public List<Student> Students { get; } = new();

        public List<Assignment> Assignment { get; } = new();
    }
}
