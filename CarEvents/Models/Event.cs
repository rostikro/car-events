using System;
using System.Collections.Generic;

namespace CarEvents.Models;

public partial class Event
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Location { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string OrganizerId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<CarEvent> CarEvents { get; set; } = new List<CarEvent>();

    public virtual ICollection<EventPhoto> EventPhotos { get; set; } = new List<EventPhoto>();

    public virtual User Organizer { get; set; } = null!;

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
