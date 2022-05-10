using System;
using CompareAction.Models;
using Xunit;

namespace TestPlan;

public class UnitTest1
{
    [Fact]
    public void TestGenerateProduct()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Product p1 = new Product(id, "Product name 1", "Some description", "2121-A", true);
        
        // Act
        // Assert
        Assert.True(p1.Id == id);
        Assert.True(p1.Name == "Product name 1");
        Assert.True(p1.Description == "Some description");
        Assert.True(p1.VendorSku == "2121-A");
        Assert.True(p1.Published == true);
    }
    
    [Fact]
    public void TestProductEqualProductTrue()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Product p1 = new Product(id, "Product name 1", "Some description", "2121-A", true);
        Product p2 = new Product(id, "Product name 1", "Some description", "2121-A", true);
        
        // Act
        var result = p1.Equals(p2);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void TestProductEqualProductObjectTrue()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Product p1 = new Product(id, "Product name 1", "Some description", "2121-A", true);
        Product p2 = new Product(id, "Product name 1", "Some description", "2121-A", true);
        
        // Act
        var result = p1.Equals((object)p2);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void TestProductHashcodeProductTrue()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Product p1 = new Product(id, "Product name 1", "Some description", "2121-A", true);
        Product p2 = new Product(id, "Product name 1", "Some description", "2121-A", true);
        
        // Act
        var p1Hashcode = p1.GetHashCode();
        var p2Hashcode = p2.GetHashCode();
        var result = p1Hashcode == p2Hashcode;
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void TestProductEqualProductFalse()
    {
        // Arrange
        Product p1 = new Product(Guid.NewGuid(), "Product name 1", "Some description", "2121-A", true);
        Product p2 = new Product(Guid.NewGuid(), "Product name 1", "Some description", "2121-A", true);
        
        // Act
        var result = p1.Equals(p2);
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void TestProductEqualProductObjectFalse()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Product p1 = new Product(Guid.NewGuid(), "Product name 1", "Some description", "2121-A", true);
        Product p2 = new Product(Guid.NewGuid(), "Product name 1", "Some description", "2121-A", true);
        
        // Act
        var result = p1.Equals((object)p2);
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void TestProductHashcodeProductFalse()
    {
        // Arrange
        Product p1 = new Product(Guid.NewGuid(), "Product name 1", "Some description", "2121-A", true);
        Product p2 = new Product(Guid.NewGuid(), "Product name 1", "Some description", "2121-A", true);
        
        // Act
        var result = p1.GetHashCode() == p2.GetHashCode();
        
        // Assert
        Assert.False(result);
    }
}