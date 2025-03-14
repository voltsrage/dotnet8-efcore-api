
namespace EFCore.API.Entities.BaseModel
{
    /// <summary>
    /// Base entity interface
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Create time timestamp
        /// </summary>
        long CreateAt { get; set; }

        /// <summary>
        /// Creator of record
        /// </summary>
        int? CreateBy { get; set; }

        /// <summary>
        /// Id of entity
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Guid of entity
        /// </summary>
        string Guid { get; set; }

        /// <summary>
        /// Displays the status of the Entity e.g. deleted, active e.g. 
        /// </summary>
        public int EntityStatusId { get; set; }

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