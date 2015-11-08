using System.ComponentModel.DataAnnotations;

namespace InstantDelivery.Core.Entities
{
    /// <summary>
    /// Klasa bazowa obiektów w bazie
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Klucz główny
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}
