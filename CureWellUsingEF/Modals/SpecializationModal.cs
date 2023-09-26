using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CureWellUsingEF.Modals
{
    public class SpecializationModal
    {
        [Key]
        [StringLength(3)]
        [Unicode(false)]
        public string SpecializationCode { get; set; } = null!;

        [StringLength(20)]
        [Unicode(false)]
        public string SpecializationName { get; set; } = null!;
    }
}
