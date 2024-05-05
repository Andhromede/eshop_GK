using System;
using System.Collections.Generic;

namespace eshop.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public bool IsActive { get; set; } = true;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
