using System;
using System.Collections.Generic;

namespace EFCore.API.Entities;

public partial class RoomType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int EntityStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
