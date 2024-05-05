using System;
using System.Collections.Generic;

namespace eshop.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Tel { get; set; }

    public string? Adress { get; set; }

    public int? Cp { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public bool IsActive { get; set; } = true;

    public virtual Role? Role { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Opinion> Opinions { get; set; } = new List<Opinion>();
}
