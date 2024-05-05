using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eshop.Models;

public partial class Role
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public bool IsActive { get; set; } = true;

    /*[JsonIgnore]*/
    /*public virtual ICollection<Client> Clients { get; set; } = new List<Client>();*/
    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
