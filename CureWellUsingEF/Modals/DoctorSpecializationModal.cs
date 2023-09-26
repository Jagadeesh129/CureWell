using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CureWellUsingEF.Modals
{
    public class DoctorSpecializationModal
    {
        [Key]
        [Column("DoctorID")]
        public int DoctorId { get; set; }

        [Key]
        [StringLength(3)]
        [Unicode(false)]
        public string SpecializationCode { get; set; } = null!;

        [Column(TypeName = "date")]
        public DateTime SpecializationDate { get; set; }

    }
}
