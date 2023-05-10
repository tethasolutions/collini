using Collini.GestioneInterventi.Domain.Docs;

namespace Collini.GestioneInterventi.Domain.Registry;

public class ProductType : BaseEntity
{
    public string? Code { get; set; }
    public string? Name { get; set; }

    public ICollection<Job> Jobs { get; set; }

    public ProductType()
    {
        Jobs = new List<Job>();
    }
}