using System;
using System.Collections.Generic;

namespace eshop.Models;

public partial class OrderDetails
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public required Order Order { get; set; }

    public required Product Product { get; set; }
}
