namespace GalleryConcept.Models;

public class Exhibit
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsSelected { get; set; }
    public string Base64Image { get; set; }
}