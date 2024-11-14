using System;
using System.Collections.Generic;

namespace CarEvents.Models;

public partial class EventPhoto
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public string PhotoUrl { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
