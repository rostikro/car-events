using System;
using System.Collections.Generic;

namespace CarEvents.Models;

public partial class Registration
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public int EventId { get; set; }

    public DateTime RegistrationDate { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
