using EFCore.API.Entities.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EFCore.API.Entities;

public class EntityStatus: BaseEntity
{
    /// <summary>
    /// Name of the status
    /// </summary>
    [StringLength(100)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Description of the status
    /// </summary>
    [StringLength(200)]
    public string Description { get; set; } = null!;
}
