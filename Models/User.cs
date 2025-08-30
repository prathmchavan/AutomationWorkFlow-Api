using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    [Column("UserId")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    public required string Password { get; set; }

    [EmailAddress]
    public string? Email { get; set; }
}
