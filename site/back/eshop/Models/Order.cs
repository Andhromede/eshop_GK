using eshop.Helpers;
using System;
using System.Collections.Generic;

namespace eshop.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? ValidationDate { get; set; }

    public DateTime? ShippingDate { get; set; }

    public required Client Client { get; set; }

    public required Status Status { get; set; }

    public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();

    /*internal object Select(Func<object, OrderDto.find> value)
    {
        throw new NotImplementedException();
    }

    public static implicit operator Order(Status v)
    {
        throw new NotImplementedException();
    }*/
}
