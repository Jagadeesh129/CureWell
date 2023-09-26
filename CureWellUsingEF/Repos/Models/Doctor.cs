using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CureWellUsingEF.Repos.Models;

[Table("Doctor")]
public partial class Doctor
{
    [Key]
    [Column("DoctorID")]
    public int DoctorId { get; set; }

    [StringLength(25)]
    [Unicode(false)]
    public string DoctorName { get; set; } = null!;

    [InverseProperty("Doctor")]
    public virtual ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();

    [InverseProperty("Doctor")]
    public virtual ICollection<Surgery> Surgeries { get; set; } = new List<Surgery>();
}
