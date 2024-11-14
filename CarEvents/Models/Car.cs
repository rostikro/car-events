using System;
using System.Collections.Generic;

namespace CarEvents.Models;

public partial class Car
{
    public int Id { get; set; }

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Year { get; set; }

    public string? Color { get; set; }

    public string? LicensePlate { get; set; }

    public virtual ICollection<CarEvent> CarEvents { get; set; } = new List<CarEvent>();

    public virtual ICollection<CarPhoto> CarPhotos { get; set; } = new List<CarPhoto>();
}
