using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace pva.Models
{
    public class Value
    {
        [Key]
        public int ValueId { get; set; }

        public int Level { get; set; }

        public DateTime Timestamp { get; set; }

        public int StationId { get; set; }

        [ForeignKey("StationId")]
        public virtual Station? Station { get; set; }
    }
}

