using Tolitech.Queryable.Extensions.UnitTests.Models;

namespace Tolitech.Queryable.Extensions.UnitTests;

/// <summary>
/// Contains unit tests for the Paginate method.
/// </summary>
public class PaginateTests
{
    /// <summary>
    /// Verifies that Paginate method returns correct page of data when one item per page is requested.
    /// </summary>
    [Fact]
    public void Paginate_OneItemPerPage_ReturnsOneElement()
    {
        // Arrange
        IQueryable<Category> data = new List<Category>
        {
            new() { Id = 1, Name = "Category A" },
            new() { Id = 2, Name = "Category B" },
            new() { Id = 3, Name = "Category C" },
        }.AsQueryable();

        // Act
        IQueryable<Category> paginatedData = data.Paginate(2, 1);

        // Assert
        Assert.Single(paginatedData); // Only one element in the page
        Assert.Equal("Category B", paginatedData.First().Name); // Should be Category B
    }
}