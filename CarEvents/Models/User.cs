using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CarEvents.Models;

public partial class User: IdentityUser
{
    public bool Premium { get; set; }
    
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
