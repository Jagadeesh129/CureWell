using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CureWellUsingEF.Modals
{
    public class SurgeryModal
    {
        [Key]
        [Column("SurgeryID")]
        public int SurgeryId { get; set; }

        [Column("DoctorID")]
        public int? DoctorId { get; set; }

        [Column(TypeName = "date")]
        public DateTime SurgeryDate { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal StartTime { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal EndTime { get; set; }

        [StringLength(3)]
        [Unicode(false)]
        public string? SurgeryCategory { get; set; }
    }
}
