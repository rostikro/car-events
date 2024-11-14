using System;
using System.Collections.Generic;

namespace CarEvents.Models;

public partial class FeatureFlag
{
    public int Id { get; set; }

    public string FeatureName { get; set; } = null!;

    public virtual ICollection<FeatureUser> FeatureUsers { get; set; } = new List<FeatureUser>();
}
