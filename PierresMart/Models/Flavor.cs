using System.Collections.Generic;

namespace PierresMart.Models
{
  public class Flavor
  {
    public Flavor()
    {
      this.JoinEntities = new HashSet<FlavorTreat>();
    }
    public int FlavorId { get; set; }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<FlavorTreat> JoinEntities { get; set; }
  }
}