using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CureWellUsingEF.Repos.Models;

[PrimaryKey("DoctorId", "SpecializationCode")]
[Table("DoctorSpecialization")]
public partial class DoctorSpecialization
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

    [ForeignKey("DoctorId")]
    [InverseProperty("DoctorSpecializations")]
    public virtual Doctor Doctor { get; set; } = null!;

    [ForeignKey("SpecializationCode")]
    [InverseProperty("DoctorSpecializations")]
    public virtual Specialization SpecializationCodeNavigation { get; set; } = null!;
}
