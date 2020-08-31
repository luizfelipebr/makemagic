using System.ComponentModel.DataAnnotations;

namespace makemagic.Dtos
{
  public class CharacterCreateDto
  {
    [Required]
    public string Name { get; set; }
    [Required]
    public string House { get; set; }
    [Required]
    public string Patronus { get; set; }
    [Required]
    public string Role { get; set; }
    [Required]
    public string School { get; set; }

  }
}