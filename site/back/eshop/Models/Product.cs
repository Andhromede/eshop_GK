using System;
using System.Collections.Generic;

namespace eshop.Models;

public partial class Product
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public decimal? Height { get; set; }

    public decimal? Width { get; set; }

    public decimal? Length { get; set; }

    public decimal? Weight { get; set; }

    public int? Capacity { get; set; }

    public decimal? Price { get; set; }

    public string? Maker { get; set; }

    public bool IsActive { get; set; } = true;

    public string? Image { get; set; }
    public string? Color { get; set; }

    public virtual ICollection<Opinion> Opinions { get; set; } = new List<Opinion>();

    public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

    public static implicit operator Product(Client v)
    {
        throw new NotImplementedException();
    }
}
