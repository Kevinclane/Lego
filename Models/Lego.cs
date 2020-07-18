namespace Legomaster.Models
{
  public class Lego
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Size { get; set; }
    public string Owner { get; set; }
    public Lego()
    {

    }
    public Lego(string name, string size)
    {
      Name = name;
      Size = size;
    }


  }
  public class LegoKit : Lego
  {
    public int LegoKitId { get; set; }
  }
}