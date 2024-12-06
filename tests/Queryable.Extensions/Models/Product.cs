namespace Tolitech.Queryable.Extensions.UnitTests.Models;

/// <summary>
/// Represents a product entity.
/// </summary>
internal sealed class Product
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; } = default!;

    /// <summary>
    /// Gets or sets the category.
    /// </summary>
    public Category? Category { get; set; }
}