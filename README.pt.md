# Tolitech.Queryable.Extensions

Uma biblioteca moderna e flexível para ordenação dinâmica e paginação de fontes de dados `IQueryable` no .NET. Ideal para desenvolvedores que desejam escrever consultas LINQ expressivas, eficientes e com o mínimo de código repetitivo.

---

## ✨ Funcionalidades

- **Ordenação Dinâmica**: Ordene coleções por qualquer propriedade (inclusive aninhada) usando uma sintaxe simples de string.
- **Ordenação Multi-nível**: Encadeie múltiplos critérios de ordenação, misturando ordens crescente e decrescente.
- **Paginação Simplificada**: Recupere qualquer página de resultados com uma única chamada de método.
- **Compatível com LINQ**: Funciona perfeitamente com provedores LINQ (Entity Framework, in-memory, etc).

---

## 🚀 Primeiros Passos

Instale o pacote NuGet (se disponível) ou adicione a referência ao seu projeto.

```csharp
using Tolitech.Queryable.Extensions;
```

---

## 🧩 Exemplos de Uso

### 1. Ordenação Dinâmica com `OrderByExpression`

Ordene seus dados dinamicamente por nomes de propriedades, inclusive propriedades aninhadas, e defina a direção da ordenação.

```csharp
var produtos = new List<Produto>
{
    new Produto { Id = 1, Nome = "Maçã", Preco = 10 },
    new Produto { Id = 2, Nome = "Laranja", Preco = 8 },
    new Produto { Id = 3, Nome = "Banana", Preco = 5 }
}.AsQueryable();

// Ordena por Nome crescente e depois por Preco decrescente
var ordenados = produtos.OrderByExpression("Nome,Preco:desc");

foreach (var p in ordenados)
    Console.WriteLine($"{p.Nome} - {p.Preco}");
```

**Saída:**
```
Maçã - 10
Banana - 5
Laranja - 8
```

#### Propriedades Aninhadas

```csharp
// Ordena por Categoria.Nome decrescente e depois por Nome crescente
produtos.OrderByExpression("Categoria.Nome:desc,Nome:asc");
```

---

### 2. Paginação com `Paginate`

Recupere uma página específica de resultados de qualquer `IQueryable`.

```csharp
var pagina2 = produtos.Paginate(pageNumber: 2, pageSize: 2);

foreach (var p in pagina2)
    Console.WriteLine($"{p.Nome} - {p.Preco}");
```

**Saída:**
```
Banana - 5
Laranja - 8
```

---

## 📚 Referência da API

### OrderByExpression

```csharp
IQueryable<T> OrderByExpression<T>(this IQueryable<T> query, string orderByString)
```
- Exemplo de `orderByString`: `"Nome:asc,Preco:desc"` ou `"Categoria.Nome:desc"`
- A ordem padrão é crescente se não especificado.

### Paginate

```csharp
IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
```
- `pageNumber`: Índice começando em 1.
- `pageSize`: Quantidade de itens por página.

---

## 💡 Dicas
- Combine `OrderByExpression` e `Paginate` para grids de dados dinâmicos e eficientes.
- Suporta propriedades aninhadas (ex: `Categoria.Nome`).
- Lida com strings de ordenação vazias ou nulas sem erros (mantém a ordem original).

---

## 🛠️ Exemplo: Consulta Completa

```csharp
var consulta = dbContext.Produtos
    .OrderByExpression("Categoria.Nome:asc,Nome:desc")
    .Paginate(pageNumber: 1, pageSize: 10);
```

---

Aproveite consultas dinâmicas, limpas e poderosas com Tolitech.Queryable.Extensions!
