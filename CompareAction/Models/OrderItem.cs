namespace CompareAction.Models;

public class OrderItem : IEquatable<OrderItem>
{
    public bool Equals(OrderItem? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id) && Product.Equals(other.Product) && Qty == other.Qty && Comment == other.Comment;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((OrderItem) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Product, Qty, Comment);
    }

    public static bool operator ==(OrderItem? left, OrderItem? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(OrderItem? left, OrderItem? right)
    {
        return !Equals(left, right);
    }

    public OrderItem(Guid id, Product product, int qty, string comment)
    {
        Id = id;
        Product = product;
        Qty = qty;
        Comment = comment;
    }

    public Guid Id { get; set; }
    public Product Product { get; set; }
    public int Qty { get; set; }
    public string Comment { get; set; }
}