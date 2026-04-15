# .NET 5 → .NET 10 Migration Audit

## Program.cs

| Method / Pattern | Deprecated? | Notes |
|---|---|---|
| `public static void Main(string[] args)` with `CreateHostBuilder(args).Build().Run()` | **Yes** | The two-class `Program` + `Startup` hosting model was superseded in .NET 6 by top-level statements and the minimal hosting model. Must be replaced with `WebApplication.CreateBuilder(args)` / `app.Run()` in a top-level `Program.cs`. |
| `public static IHostBuilder CreateHostBuilder(string[] args)` | **Yes** | `IHostBuilder` / `Host.CreateDefaultBuilder()` bootstrapping is replaced by `WebApplication.CreateBuilder()`. The method itself should be removed entirely. |
| `Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })` | **Yes** | `.ConfigureWebHostDefaults` + `.UseStartup<T>()` are replaced by the `WebApplicationBuilder` API. Remove and rewrite as `var builder = WebApplication.CreateBuilder(args); var app = builder.Build();`. |

---

## Startup.cs

| Method / Pattern | Deprecated? | Notes |
|---|---|---|
| `public class Startup` (the class itself) | **Yes** | The `Startup` class convention (`ConfigureServices` + `Configure`) was retired in .NET 6. In .NET 10 all startup code lives directly in `Program.cs`. The entire file can be deleted after migration. |
| `public void ConfigureServices(IServiceCollection services)` | **Yes** | Replace with `builder.Services.*` calls in `Program.cs` (e.g., `builder.Services.AddControllers()`). |
| `public void Configure(IApplicationBuilder app, IWebHostEnvironment env)` | **Yes** | Replace with direct middleware calls on the `WebApplication` instance (`app.UseHttpsRedirection()`, etc.) in `Program.cs`. |
| `app.UseEndpoints(endpoints => { endpoints.MapControllers(); })` | **Yes** | `UseEndpoints` is deprecated. Replace with the top-level `app.MapControllers()` call introduced in .NET 6. |
| `app.UseDeveloperExceptionPage()` | **Soft-deprecated** | In .NET 8+ the developer exception page is enabled automatically when the environment is `Development`. The call is harmless but redundant and should be removed. |
| `using Microsoft.AspNetCore.HttpsPolicy;` | **Yes – breaks build** | The `Microsoft.AspNetCore.HttpsPolicy` namespace was removed in .NET 8. The `using` directive must be deleted; HTTPS redirection is still available via `app.UseHttpsRedirection()` without this import. |
| `services.AddCors()` + `app.UseCors(...)` (separate registration) | **No – still valid, but package must change** | The logic is fine; however, the `Microsoft.AspNetCore.Cors` NuGet package (v2.2.0) is obsolete – CORS has been bundled into `Microsoft.AspNetCore.App` since .NET Core 3.0 and the standalone package must be removed. |
| `services.AddSwaggerGen(c => { c.SwaggerDoc(...) })` via Swashbuckle | **Soft-deprecated** | Swashbuckle.AspNetCore v5.6.3 is incompatible with .NET 10. Either upgrade to Swashbuckle v7+ or, preferably, migrate to the built-in OpenAPI support added in .NET 9 (`builder.Services.AddOpenApi()` / `app.MapOpenApi()`). |

---

## Controllers/BaseApiController.cs

| Method / Pattern | Deprecated? | Notes |
|---|---|---|
| `protected IActionResult HandleException(Exception ex, string msg)` | **No** | The method is still valid in .NET 10. However, serialising an `Exception` object directly into a response body is bad practice; replace with `Problem()` (RFC 7807 `ProblemDetails`) for standards-compliant error responses. |

---

## Controllers/ProductController.cs

| Method / Pattern | Deprecated? | Notes |
|---|---|---|
| `public IActionResult Get()` | **No – but pattern is outdated** | Returns `IActionResult`. Prefer the generic `ActionResult<List<Product>>` which enables accurate OpenAPI schema generation in .NET 10. |
| `public IActionResult Get(int id)` | **No – but pattern is outdated** | Same as above; prefer `ActionResult<Product>`. |
| `public IActionResult Post(Product entity)` | **No – but pattern is outdated** | Same as above; prefer `ActionResult<Product>`. Also consider adding `[FromBody]` explicitly for clarity. |
| `public IActionResult Put(int id, Product entity)` | **No – but pattern is outdated** | Same as above; prefer `ActionResult<Product>`. |
| `public IActionResult Delete(int id)` | **No** | Still valid. |
| `_DbContext.Products.Count() > 0` | **No – but inefficient** | `Count()` fetches a count from the DB. Replace with `_DbContext.Products.Any()` which is more efficient and semantically clearer. |
| `_DbContext.Products.Find(id)` (synchronous) | **No – but discouraged** | Synchronous EF Core calls block the thread pool. Replace with `await _DbContext.Products.FindAsync(id)` and make the action `async Task<IActionResult>`. |
| `_DbContext.Products.OrderBy(p => p.Name).ToList()` (synchronous) | **No – but discouraged** | Replace with `await _DbContext.Products.OrderBy(p => p.Name).ToListAsync()`. |
| `_DbContext.SaveChanges()` (synchronous) | **No – but discouraged** | Replace with `await _DbContext.SaveChangesAsync()`. |
| `DateTime.Now` (used for `ModifiedDate`) | **No** | Still works but `DateTime.Now` returns local time, which is problematic in cloud environments. Prefer `DateTime.UtcNow` or `DateTimeOffset.UtcNow`. |
| `IActionResult ret = null;` (nullable without annotation) | **Warning in .NET 10** | With nullable reference types (NRTs) enabled by default in .NET 10 projects, assigning `null` to a non-nullable `IActionResult` generates a warning. Change to `IActionResult? ret = null;`. |

---

## Models/AdventureWorksLTDbContext.cs

| Method / Pattern | Deprecated? | Notes |
|---|---|---|
| `public AdventureWorksLTDbContext(DbContextOptions<AdventureWorksLTDbContext> options) : base(options)` | **No** | Constructor injection via `DbContextOptions` is still the correct EF Core pattern in .NET 10. |
| `public virtual DbSet<Product> Products { get; set; }` | **Warning in .NET 10** | Without nullable annotations, the compiler warns that the property is uninitialized. Change to `public virtual DbSet<Product> Products { get; set; } = null!;` or use the primary constructor overload. |
| `protected override void OnModelCreating(ModelBuilder modelBuilder)` | **No** | Still the correct hook for Fluent API configuration in EF Core 10. |

---

## EntityClasses/Product.cs

| Property / Pattern | Deprecated? | Notes |
|---|---|---|
| `public string Name { get; set; }` (and all other non-nullable `string` properties) | **Warning in .NET 10** | Non-nullable reference type properties without initializers generate CS8618 warnings when NRTs are enabled. Add `= string.Empty;` initialisers or mark them nullable where appropriate. |
| `[Required]` on value types (`decimal`, `int`) | **No** | Still valid; redundant on non-nullable value types but harmless. |

---

## WebAPI.csproj — NuGet Package Changes Required

| Package | Current Version | Action Required |
|---|---|---|
| `Microsoft.AspNetCore.Cors` | 2.2.0 | **Remove** — CORS is built into the ASP.NET Core shared framework since 3.0; this package is obsolete and will cause conflicts. |
| `Microsoft.EntityFrameworkCore.SqlServer` | 5.0.2 | **Upgrade to 9.0.x or 10.0.x** — EF Core 5 is out of support and incompatible with .NET 10 runtime improvements. |
| `Swashbuckle.AspNetCore` | 5.6.3 | **Remove or upgrade** — v5 is incompatible with .NET 10. Migrate to the built-in `Microsoft.AspNetCore.OpenApi` (available from .NET 9) or upgrade to Swashbuckle v7+. |
| `System.Text.Json` | 5.0.1 | **Remove** — `System.Text.Json` is included in the .NET 10 runtime; the standalone package is not needed and pins an outdated version. |

---

## Next Steps: Migrating from .NET 5 to .NET 10

The following actions are required, roughly in priority order:

1. **Update the target framework** — `WebAPI.csproj` already targets `net10.0`. Verify this builds cleanly after all other changes are applied.

2. **Remove/update NuGet packages** — Apply the package changes in the table above. Run `dotnet restore` after editing the `.csproj` to verify resolution.

3. **Delete `Startup.cs` and rewrite `Program.cs`** — Migrate all `ConfigureServices` and `Configure` logic into a single `Program.cs` using the minimal hosting model:
   ```csharp
   var builder = WebApplication.CreateBuilder(args);
   builder.Services.AddControllers()...;
   builder.Services.AddDbContext<AdventureWorksLTDbContext>(...);
   // etc.
   var app = builder.Build();
   app.UseHttpsRedirection();
   app.UseAuthorization();
   app.UseCors(...);
   app.MapControllers();
   app.Run();
   ```

4. **Remove the `Microsoft.AspNetCore.HttpsPolicy` using directive** from `Startup.cs` (or simply delete the file after step 3).

5. **Migrate Swagger/OpenAPI** — Replace Swashbuckle with the built-in OpenAPI middleware:
   ```csharp
   builder.Services.AddOpenApi();
   // ...
   app.MapOpenApi();
   ```

6. **Enable nullable reference types** — Add `<Nullable>enable</Nullable>` to the `.csproj` and fix all CS8618 warnings in `Product.cs`, `AdventureWorksLTDbContext.cs`, and the controller files.

7. **Make all EF Core and controller calls async** — Convert `Find`, `ToList`, `SaveChanges`, and `Count` to their `Async` counterparts, and update controller action signatures to `async Task<ActionResult<T>>`.

8. **Replace `IActionResult` with `ActionResult<T>`** — Update controller return types for better OpenAPI schema generation and compile-time safety.

9. **Replace `DateTime.Now` with `DateTime.UtcNow`** — In `ProductController.cs` (Post and Put actions) to avoid timezone-related bugs in cloud/container deployments.

10. **Run `dotnet build`** and address any remaining compiler warnings or errors, then run integration tests against a real SQL Server instance to validate EF Core 10 compatibility with the existing `AdventureWorksLT` schema.
