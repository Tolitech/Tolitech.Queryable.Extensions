# Tolitech.Queryable.Extensions

The `Tolitech.Queryable.Extensions` library provides utility methods to simplify querying operations on `IQueryable` data sources. It includes features for dynamic ordering and pagination, enabling efficient data retrieval and manipulation in LINQ queries.

---

## Features

### 1. **Dynamic Ordering**
   - Allows sorting `IQueryable` collections dynamically based on property names specified in a string.
   - Supports multi-level sorting with multiple fields and ascending/descending order.

### 2. **Pagination**
   - Provides an easy way to implement paging logic with parameters for page number and page size.

---

## Usage

### 1. **Dynamic Ordering with `OrderByExpression`**

Use the `OrderByExpression` method to dynamically sort an `IQueryable` collection based on a string that specifies the property and order.

#### Syntax

```csharp
public static IQueryable<T> OrderByExpression<T>(
    this IQueryable<T> query, 
    string orderByString
)
```

- **`query`**: The data source to sort.
- **`orderByString`**: A comma-separated string specifying the sorting criteria. Use `:` to indicate sorting order (`asc` or `desc`). Default is `asc`.

#### Example

```csharp
using Tolitech.Queryable.Extensions;

// Sample data
var products = new List<Product>
{
    new Product { Id = 1, Name = "Apple", Price = 10 },
    new Product { Id = 2, Name = "Orange", Price = 8 },
    new Product { Id = 3, Name = "Banana", Price = 5 }
}.AsQueryable();

// Order by Name (ascending) and then Price (descending)
string orderByString = "Name,Price:desc";

var sortedProducts = products.OrderByExpression(orderByString);

foreach (var product in sortedProducts)
{
    Console.WriteLine($"{product.Name} - {product.Price}");
}
```

#### Output

```plaintext
Apple - 10
Banana - 5
Orange - 8
```

---

### 2. **Pagination with `Paginate`**

Use the `Paginate` method to retrieve a specific page of items from an `IQueryable` collection.

#### Syntax

```csharp
public static IQueryable<T> Paginate<T>(
    this IQueryable<T> source, 
    int pageNumber, 
    int pageSize
)
```

- **`source`**: The data source to paginate.
- **`pageNumber`**: The page number to retrieve (starting at 1).
- **`pageSize`**: The number of items per page.

#### Example

```csharp
using Tolitech.Queryable.Extensions;

// Sample data
var products = new List<Product>
{
    new Product { Id = 1, Name = "Apple", Price = 10 },
    new Product { Id = 2, Name = "Orange", Price = 8 },
    new Product { Id = 3, Name = "Banana", Price = 5 },
    new Product { Id = 4, Name = "Grapes", Price = 15 },
    new Product { Id = 5, Name = "Pineapple", Price = 20 }
}.AsQueryable();

// Paginate data: Page 2, 2 items per page
int pageNumber = 2;
int pageSize = 2;

var paginatedProducts = products.Paginate(pageNumber, pageSize);

foreach (var product in paginatedProducts)
{
    Console.WriteLine($"{product.Name} - {product.Price}");
}
```

#### Output

```plaintext
Banana - 5
Grapes - 15
```

---

## API Reference

### 1. `OrderByExpression`

- Dynamically sorts the `IQueryable` based on the specified string.
- Supports multi-level sorting using a comma-separated list.
- Default sorting order is ascending (`asc`).

**Example Input Strings:**
- `"Name"`: Sorts by `Name` ascending.
- `"Price:desc"`: Sorts by `Price` descending.
- `"Category,Price:desc"`: Sorts by `Category` ascending, then by `Price` descending.

---

### 2. `Paginate`

- Implements paging logic for `IQueryable` data sources.
- Skips records based on `pageNumber` and retrieves `pageSize` number of items.

**Example Parameters:**
- `pageNumber = 1, pageSize = 5`: Returns the first 5 items.
- `pageNumber = 2, pageSize = 3`: Skips the first 3 items and returns the next 3 items.