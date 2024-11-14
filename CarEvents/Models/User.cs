using System;
using System.Collections.Generic;

namespace CarEvents.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FullName { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<FeatureUser> FeatureUsers { get; set; } = new List<FeatureUser>();

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
