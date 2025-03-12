using System;
using System.Collections.Generic;

namespace EFCore.API.Entities;

public partial class Hotel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public int EntityStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
