using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CureWellUsingEF.Repos.Models;

[Table("RefreshToken")]
public partial class RefreshToken
{
    [Key]
    [Column("userId")]
    [StringLength(50)]
    [Unicode(false)]
    public string UserId { get; set; } = null!;

    [Column("tokenId")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TokenId { get; set; }

    [Column("refreshtoken")]
    [Unicode(false)]
    public string? Refreshtoken1 { get; set; }
}
