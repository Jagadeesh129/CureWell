using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CureWellUsingEF.Repos.Models;

[Table("_User")]
[Index("UserName", Name = "UQ___User__C9F28456F1F92F29", IsUnique = true)]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string UserName { get; set; } = null!;

    [StringLength(30)]
    public string Password { get; set; } = null!;

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    public string? Email { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Mobile { get; set; }

    [StringLength(30)]
    public string? Role { get; set; }
}
