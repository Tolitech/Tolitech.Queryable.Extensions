namespace Tolitech.Queryable.Extensions.UnitTests.Models;

/// <summary>
/// Represents a category entity.
/// </summary>
internal sealed class Category
{
    /// <summary>
    /// Gets or sets the unique identifier of the category.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the category.
    /// </summary>
    public string Name { get; set; } = default!;
}