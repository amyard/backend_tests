namespace CompareAction.Models;

public class Product
{
    protected bool Equals(Product other)
    {
        return Id.Equals(other.Id) && Name == other.Name && Description == other.Description && VendorSku == other.VendorSku && Published == other.Published;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Product) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Description, VendorSku, Published);
    }

    public Product(Guid id, string name, string description, string vendorSku, bool published)
    {
        Id = id;
        Name = name;
        Description = description;
        VendorSku = vendorSku;
        Published = published;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string VendorSku { get; set; }
    public bool Published { get; set; }
}