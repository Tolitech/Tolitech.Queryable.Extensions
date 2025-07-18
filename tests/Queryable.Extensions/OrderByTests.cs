using Tolitech.Queryable.Extensions.UnitTests.Models;

namespace Tolitech.Queryable.Extensions.UnitTests;

/// <summary>
/// Contains unit tests for the OrderByExpression method.
/// </summary>
public class OrderByTests
{
    /// <summary>
    /// Verifies that OrderByExpression method sorts the data correctly when one sorting criteria is provided.
    /// </summary>
    [Fact]
    public void OrderByExpression_WithoutSort_SameOrder()
    {
        // Arrange
        IQueryable<Category> data = new List<Category>
        {
            new() { Id = 2, Name = "Category B" },
            new() { Id = 1, Name = "Category A" },
        }.AsQueryable();

        // Act
        IQueryable<Category> sortedData = data.OrderByExpression(string.Empty);

        // Assert
        Assert.Equal("Category B", sortedData.First().Name);
        Assert.Equal("Category A", sortedData.Last().Name);
    }

    /// <summary>
    /// Verifies that OrderByExpression method sorts the data correctly when one sorting criteria is provided.
    /// </summary>
    [Fact]
    public void OrderByExpression_WithOneSort_SortAscending()
    {
        // Arrange
        IQueryable<Category> data = new List<Category>
        {
            new() { Id = 2, Name = "Category B" },
            new() { Id = 1, Name = "Category A" },
        }.AsQueryable();

        // Act
        IQueryable<Category> sortedData = data.OrderByExpression("Name");

        // Assert
        Assert.Equal("Category A", sortedData.First().Name);
        Assert.Equal("Category B", sortedData.Last().Name);
    }

    /// <summary>
    /// Verifies that OrderByExpression method sorts the data correctly when one sorting criteria is provided.
    /// </summary>
    [Fact]
    public void OrderByExpression_AscWithOneSort_SortAscending()
    {
        // Arrange
        IQueryable<Category> data = new List<Category>
        {
            new() { Id = 1, Name = "Category A" },
            new() { Id = 2, Name = "Category C" },
            new() { Id = 3, Name = "Category B" },
        }.AsQueryable();

        // Act
        IQueryable<Category> sortedData = data.OrderByExpression("Name:asc");

        // Assert
        Assert.Equal("Category A", sortedData.First().Name);
        Assert.Equal("Category C", sortedData.Last().Name);
    }

    /// <summary>
    /// Verifies that OrderByExpression method sorts the data in descending order when one sorting criteria with descending order is provided.
    /// </summary>
    [Fact]
    public void OrderByExpression_DescWithOneSort_SortDescending()
    {
        // Arrange
        IQueryable<Category> data = new List<Category>
        {
            new() { Id = 1, Name = "Category A" },
            new() { Id = 2, Name = "Category C" },
            new() { Id = 3, Name = "Category B" },
        }.AsQueryable();

        // Act
        IQueryable<Category> sortedData = data.OrderByExpression("Name:desc");

        // Assert
        Assert.Equal("Category C", sortedData.First().Name);
        Assert.Equal("Category A", sortedData.Last().Name);
    }

    /// <summary>
    /// Verifies that OrderByExpression method sorts the data in ascending order based on a nested property.
    /// </summary>
    [Fact]
    public void OrderByExpression_NestedAsc_SortAscending()
    {
        // Arrange
        Category categoryA = new() { Id = 1, Name = "Category A" };
        Category categoryB = new() { Id = 2, Name = "Category B" };

        IQueryable<Product> data = new List<Product>
        {
            new() { Id = 1, Name = "Product A", Category = categoryB },
            new() { Id = 2, Name = "Product B", Category = categoryA },
        }.AsQueryable();

        // Act
        IQueryable<Product> sortedData = data.OrderByExpression("Category.Name:asc");

        // Assert
        Assert.Equal("Product B", sortedData.First().Name);
        Assert.Equal("Product A", sortedData.Last().Name);
    }

    /// <summary>
    /// Verifies that OrderByExpression method sorts the data in descending order based on a nested property.
    /// </summary>
    [Fact]
    public void OrderByExpression_NestedDesc_SortDescending()
    {
        // Arrange
        Category categoryA = new() { Id = 1, Name = "Category A" };
        Category categoryB = new() { Id = 2, Name = "Category B" };

        IQueryable<Product> data = new List<Product>
        {
            new() { Id = 1, Name = "Product A", Category = categoryB },
            new() { Id = 2, Name = "Product B", Category = categoryA },
        }.AsQueryable();

        // Act
        IQueryable<Product> sortedData = data.OrderByExpression("Category.Name:desc");

        // Assert
        Assert.Equal("Product A", sortedData.First().Name);
        Assert.Equal("Product B", sortedData.Last().Name);
    }

    /// <summary>
    /// Verifies that OrderByExpression method sorts the data based on multiple sorting criteria, ascending and descending.
    /// </summary>
    [Fact]
    public void OrderByExpression_MultipleSorting_SortAscAndDesc()
    {
        // Arrange
        Category categoryA = new() { Id = 1, Name = "Category A" };
        Category categoryB = new() { Id = 2, Name = "Category B" };

        IQueryable<Product> data = new List<Product>
        {
            new() { Id = 5, Name = "Product E", Category = categoryB },
            new() { Id = 1, Name = "Product A", Category = categoryB },
            new() { Id = 4, Name = "Product D", Category = categoryA },
            new() { Id = 2, Name = "Product B", Category = categoryB },
            new() { Id = 3, Name = "Product C", Category = categoryA },
        }.AsQueryable();

        // Act
        IQueryable<Product> sortedData = data.OrderByExpression("Category.Name:asc, Name:desc");

        // Assert
        Assert.Equal("Product D", sortedData.First().Name);
        Assert.Equal("Product A", sortedData.Last().Name);
    }

    /// <summary>
    /// Verifies that OrderByExpression method sorts the data based on multiple sorting criteria, ascending and descending.
    /// </summary>
    [Fact]
    public void OrderByExpression_MultipleSorting_SortDescAndAsc()
    {
        // Arrange
        Category categoryA = new() { Id = 1, Name = "Category A" };
        Category categoryB = new() { Id = 2, Name = "Category B" };

        IQueryable<Product> data = new List<Product>
        {
            new() { Id = 5, Name = "Product E", Category = categoryB },
            new() { Id = 1, Name = "Product A", Category = categoryB },
            new() { Id = 4, Name = "Product D", Category = categoryA },
            new() { Id = 2, Name = "Product B", Category = categoryB },
            new() { Id = 3, Name = "Product C", Category = categoryA },
        }.AsQueryable();

        // Act
        IQueryable<Product> sortedData = data.OrderByExpression("Category.Name:desc, Name:asc");

        // Assert
        Assert.Equal("Product A", sortedData.First().Name);
        Assert.Equal("Product D", sortedData.Last().Name);
    }

    /// <summary>
    /// Verifies that OrderByExpression throws an ArgumentException when a property does not exist.
    /// </summary>
    [Fact]
    public void OrderByExpression_InvalidProperty_ThrowsArgumentException()
    {
        // Arrange
        IQueryable<Category> data = new List<Category>
        {
            new() { Id = 1, Name = "Category A" },
            new() { Id = 2, Name = "Category B" },
        }.AsQueryable();

        // Act
        ArgumentException ex = Assert.Throws<ArgumentException>(() => data.OrderByExpression("NonExistentProperty"));

        // Assert
        Assert.Contains("Property 'NonExistentProperty' not found", ex.Message, StringComparison.Ordinal);
    }
}