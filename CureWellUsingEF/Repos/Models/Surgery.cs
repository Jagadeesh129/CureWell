using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CureWellUsingEF.Repos.Models;

[Table("Surgery")]
public partial class Surgery
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

    [ForeignKey("DoctorId")]
    [InverseProperty("Surgeries")]
    public virtual Doctor? Doctor { get; set; }

    [ForeignKey("SurgeryCategory")]
    [InverseProperty("Surgeries")]
    public virtual Specialization? SurgeryCategoryNavigation { get; set; }
}
