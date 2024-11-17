namespace APIStore.Dtos;

public class CategoryDto
{
  public string Name { get; set; }

  public CategoryDto() { }

  public CategoryDto(string name)
  {
    this.Name = name;
  }
}