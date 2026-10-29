# NetCoreApiWeb — N-layer ASP.NET Core Web API (.NET 8)

A reference ASP.NET Core Web API for a small product catalogue (categories and products), organised in three layers and built around common best practices.

## What it shows

- **Layers:** `App.API` (controllers), `App.Services` (business logic, DTOs, validation, mapping) and `App.Repositories` (EF Core data access).
- **Validation:** FluentValidation with an action filter and consistent `ApiBehaviorOptions`.
- **Error handling:** global and critical exception handlers that return a uniform `ServiceResult`.
- **Data access:** EF Core with SQL Server, migrations, a generic repository and a unit of work.
- **Auditing:** an EF Core `SaveChanges` interceptor that stamps created/updated dates.
- **Mapping:** AutoMapper profiles per feature.

## Run locally

1. Install the .NET 8 SDK and SQL Server Express LocalDB (or change the connection string in `App.API/appsettings.json`).
2. Create the database:
   ```bash
   dotnet ef database update --project Repositories --startup-project App.API
   ```
3. Start the API and open the Swagger UI it prints:
   ```bash
   dotnet run --project App.API
   ```

## License

MIT — see [LICENSE.txt](LICENSE.txt).
