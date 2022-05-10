using System;
using CompareAction.Models;
using Xunit;

namespace TestPlan;

public class UnitTestOrderItem
{
    [Fact]
    public void TestOrderItemEqualsTrue()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Product p1 = new Product(Guid.NewGuid(), "Product name 1", "Some description", "2121-A", true);
        OrderItem i1 = new OrderItem(id, p1, 1, "Some comment");
        OrderItem i2 = new OrderItem(id, p1, 1, "Some comment");
        
        
        // Act
        var result = i1.Equals(i2);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void TestOrderItemEqualsTrueDifferentGuid()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Product p1 = new Product(Guid.NewGuid(), "Product name 1", "Some description", "2121-A", true);
        OrderItem i1 = new OrderItem(id, p1, 1, "Some comment");
        OrderItem i2 = new OrderItem(id, p1, 1, "Some comment");
        
        
        // Act
        var result = i1.Equals(i2);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void TestOrderItemObjectEqualsTrue()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Product p1 = new Product(Guid.NewGuid(), "Product name 1", "Some description", "2121-A", true);
        OrderItem i1 = new OrderItem(id, p1, 1, "Some comment");
        OrderItem i2 = new OrderItem(id, p1, 1, "Some comment");
        
        
        // Act
        var result = i1.Equals((object)i2);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void TestOrderItemHashcodeTrue()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Product p1 = new Product(Guid.NewGuid(), "Product name 1", "Some description", "2121-A", true);
        OrderItem i1 = new OrderItem(id, p1, 1, "Some comment");
        OrderItem i2 = new OrderItem(id, p1, 1, "Some comment");
        
        
        // Act
        var result = i1.GetHashCode() == i2.GetHashCode();
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void TestOrderItemOperatorTrue()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Product p1 = new Product(Guid.NewGuid(), "Product name 1", "Some description", "2121-A", true);
        OrderItem i1 = new OrderItem(id, p1, 1, "Some comment");
        OrderItem i2 = new OrderItem(id, p1, 1, "Some comment");
        
        
        // Act
        var result = i1 == i2;
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void TestOrderItemObjectEqualsFalse()
    {
        // Arrange
        Product p1 = new Product(Guid.NewGuid(), "Product name 1", "Some description", "2121-A", true);
        OrderItem i1 = new OrderItem(Guid.NewGuid(), p1, 1, "Some comment");
        OrderItem i2 = new OrderItem(Guid.NewGuid(), p1, 2, "Some comment");
        
        
        // Act
        var result = i1.Equals((object)i2);
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void TestOrderItemHashcodeFalse()
    {
        // Arrange
        Product p1 = new Product(Guid.NewGuid(), "Product name 1", "Some description", "2121-A", true);
        OrderItem i1 = new OrderItem(Guid.NewGuid(), p1, 1, "Some comment");
        OrderItem i2 = new OrderItem(Guid.NewGuid(), p1, 2, "Some comment");
        
        
        // Act
        var result = i1.GetHashCode() == i2.GetHashCode();
        
        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void TestOrderItemOperatorFalse()
    {
        // Arrange
        Product p1 = new Product(Guid.NewGuid(), "Product name 1", "Some description", "2121-A", true);
        OrderItem i1 = new OrderItem(Guid.NewGuid(), p1, 1, "Some comment");
        OrderItem i2 = new OrderItem(Guid.NewGuid(), p1, 2, "Some comment");
        
        
        // Act
        var result = i1 == i2;
        
        // Assert
        Assert.False(result);
    }
}