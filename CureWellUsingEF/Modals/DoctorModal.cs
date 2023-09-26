using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CureWellUsingEF.Modals
{
    public class DoctorModal
    {
        [Key]
        [Column("DoctorID")]
        public int DoctorId { get; set; }

        [StringLength(25)]
        [Unicode(false)]
        public string DoctorName { get; set; } = null!;
    }
}
