namespace WebApplication2.Models
{
    public class OrderExam
    {

        public int OrderId { get; set; }
        public int ExamId { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Exam Exam { get; set; } = null!;
    }
}
