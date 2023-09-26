using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CureWellUsingEF.Repos.Models;

[Table("Specialization")]
public partial class Specialization
{
    [Key]
    [StringLength(3)]
    [Unicode(false)]
    public string SpecializationCode { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string SpecializationName { get; set; } = null!;

    [InverseProperty("SpecializationCodeNavigation")]
    public virtual ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();

    [InverseProperty("SurgeryCategoryNavigation")]
    public virtual ICollection<Surgery> Surgeries { get; set; } = new List<Surgery>();
}
