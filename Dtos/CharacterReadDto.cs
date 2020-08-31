using System.ComponentModel.DataAnnotations;

namespace makemagic.Dtos
{
  public class CharacterReadDto
  {

    public int Id { get; set; }
    public string Name { get; set; }
    public string House { get; set; }
    public string Patronus { get; set; }
    public string Role { get; set; }
    public string School { get; set; }

  }
}