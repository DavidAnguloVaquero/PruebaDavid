using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Patient
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
