using EFCore.API.Entities.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFCore.API.Entities;

public class RoomType : BaseEntity
{
    /// Name of room type
    /// </summary>
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description of room type
    /// </summary>
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// List of rooms of this room type
    /// </summary>
    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
