# Tolitech.Queryable.Extensions

A modern, flexible library for dynamic sorting and pagination of `IQueryable` data sources in .NET. Designed for developers who want to write expressive, maintainable, and efficient LINQ queries with minimal boilerplate.

---

## ✨ Features

- **Dynamic Ordering**: Sort collections by any property (including nested) using a simple string syntax.
- **Multi-level Sorting**: Chain multiple sort criteria, mixing ascending and descending orders.
- **Effortless Pagination**: Retrieve any page of results with a single method call.
- **LINQ-Friendly**: Works seamlessly with LINQ providers (Entity Framework, in-memory, etc).

---

## 🚀 Getting Started

Install the NuGet package (if available) or add a reference to your project.

```csharp
using Tolitech.Queryable.Extensions;
```

---

## 🧩 Usage Examples

### 1. Dynamic Ordering with `OrderByExpression`

Sort your data dynamically by property names, including nested properties, and specify the order direction.

```csharp
var products = new List<Product>
{
    new Product { Id = 1, Name = "Apple", Price = 10 },
    new Product { Id = 2, Name = "Orange", Price = 8 },
    new Product { Id = 3, Name = "Banana", Price = 5 }
}.AsQueryable();

// Sort by Name ascending, then by Price descending
var sorted = products.OrderByExpression("Name,Price:desc");

foreach (var p in sorted)
    Console.WriteLine($"{p.Name} - {p.Price}");
```

**Output:**
```
Apple - 10
Banana - 5
Orange - 8
```

#### Nested Properties

```csharp
// Sort by Category.Name descending, then by Name ascending
products.OrderByExpression("Category.Name:desc,Name:asc");
```

---

### 2. Pagination with `Paginate`

Retrieve a specific page of results from any `IQueryable`.

```csharp
var page2 = products.Paginate(pageNumber: 2, pageSize: 2);

foreach (var p in page2)
    Console.WriteLine($"{p.Name} - {p.Price}");
```

**Output:**
```
Banana - 5
Grapes - 15
```

---

## 📚 API Reference

### OrderByExpression

```csharp
IQueryable<T> OrderByExpression<T>(this IQueryable<T> query, string orderByString)
```
- `orderByString` example: `"Name:asc,Price:desc"` or `"Category.Name:desc"`
- Default order is ascending if not specified.

### Paginate

```csharp
IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
```
- `pageNumber`: 1-based index.
- `pageSize`: Number of items per page.

---

## 💡 Tips
- Combine `OrderByExpression` and `Paginate` for efficient, user-driven data grids.
- Supports nested properties (e.g., `Category.Name`).
- Handles empty or null sort strings gracefully (returns original order).

---

## 🛠️ Example: Full Query

```csharp
var query = dbContext.Products
    .OrderByExpression("Category.Name:asc,Name:desc")
    .Paginate(pageNumber: 1, pageSize: 10);
```

---

Enjoy clean, dynamic, and powerful querying with Tolitech.Queryable.Extensions!