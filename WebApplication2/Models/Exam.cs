using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Exam
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
