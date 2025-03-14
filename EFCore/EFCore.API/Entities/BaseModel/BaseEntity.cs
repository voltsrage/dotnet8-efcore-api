using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.API.Entities.BaseModel
{
    /// <summary>
    /// Implement base entity
    /// </summary>
    public class BaseEntity : IEntity
    {
        /// <summary>
        /// Id of entity
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Displays the status of the Entity e.g. deleted, active e.g. 
        /// </summary>
        public int EntityStatusId { get; set; } = 1;

        /// <summary>
        /// CreateAt    
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// When this record is updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// who create this record
        /// </summary>
        public int? CreatedBy { get; set; }

        /// <summary>
        /// who update this record
        /// </summary>
        public int? UpdatedBy { get; set; }
    }
}
