using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class CreateOrderRequest
    {
        [Required]
        [MinLength(3)]
        public string PatientName { get; set; }

        [Required]
        public DateTime AttentionDate { get; set; }

        [Required]
        [MinLength(1)]
        public List<CreateExamenRequest> Exams { get; set; }

    }


    public class CreateExamenRequest
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
