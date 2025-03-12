using System;
using System.Collections.Generic;

namespace EFCore.API.Entities;

public partial class Room
{
    public int Id { get; set; }

    public int HotelId { get; set; }

    public string RoomNumber { get; set; } = null!;

    public int RoomTypeId { get; set; }

    public decimal PricePerNight { get; set; }

    public bool IsAvailable { get; set; }

    public int MaxOccupancy { get; set; }

    public int EntityStatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual RoomType RoomType { get; set; } = null!;
}
