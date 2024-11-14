using System;
using System.Collections.Generic;

namespace CarEvents.Models;

public partial class FeatureUser
{
    public int Id { get; set; }

    public int FeatureId { get; set; }

    public int UserId { get; set; }

    public virtual FeatureFlag Feature { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
