using System;
using System.Collections.Generic;

namespace CarEvents.Models;

public partial class CarEvent
{
    public int Id { get; set; }

    public int CarId { get; set; }

    public int EventId { get; set; }

    public virtual Car Car { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
