using System.ComponentModel.DataAnnotations.Schema;
using PBCW2.Base.Entity;

[Table("Product", Schema = "dbo")]
public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}