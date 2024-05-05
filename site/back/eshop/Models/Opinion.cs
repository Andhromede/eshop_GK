using System;
using System.Collections.Generic;

namespace eshop.Models;

public partial class Opinion
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public bool IsValidate { get; set; } = false;

    public bool IsModerate { get; set; } = false;

    public virtual Product? Product { get; set; }
    public virtual Client? Client { get; set; }
}
