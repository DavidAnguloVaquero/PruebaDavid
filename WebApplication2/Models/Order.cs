using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Order
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public DateTime? AttentionDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();
}

