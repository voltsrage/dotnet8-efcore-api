﻿using EFCore.API.Entities.BaseModel;

namespace EFCore.API.Entities;

public class Room : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier of the hotel to which the room belongs.
    /// </summary>
    public int HotelId { get; set; }

    /// <summary>
    /// Gets or sets the room number.
    /// </summary>
    public string RoomNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of the room (e.g., Standard, Deluxe, Suite).
    /// </summary>
    public int RoomTypeId { get; set; }

    /// <summary>
    /// Gets or sets the price per night for the room.
    /// </summary>
    public decimal PricePerNight { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the room is available.
    /// </summary>
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Gets or sets the maximum number of guests that can occupy the room.
    /// </summary>
    public int MaxOccupancy { get; set; }

    public virtual Hotel Hotel { get; set; } = null!;

    public virtual RoomType RoomType { get; set; } = null!;
}
