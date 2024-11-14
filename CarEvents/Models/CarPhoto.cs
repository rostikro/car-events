using System;
using System.Collections.Generic;

namespace CarEvents.Models;

public partial class CarPhoto
{
    public int Id { get; set; }

    public int CarId { get; set; }

    public string PhotoUrl { get; set; } = null!;

    public virtual Car Car { get; set; } = null!;
}
