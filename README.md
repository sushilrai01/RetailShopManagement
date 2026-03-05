# RetailShopManagement

This is a **Blazor/.NET 8** web application using **Entity Framework Core** implementing the **CQRS Design Pattern** and following **Clean Architecture** principles. This guide explains how to set up the project locally and run database migrations.

---

## Prerequisites

Before running the project, ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or VS Code
- SQL Server (or your preferred database)
- Optional: `dotnet-ef` CLI tools for migrations

---

## Project Setup

1. **Clone the repository:**

```bash
git clone https://github.com/yourusername/your-repo.git
cd your-repo
```

2. **Update `appsettings.Development.json`**

Open appsettings.Development.json and update the connection string for your local database:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```
3. **Set Startup Project**

If the solution has multiple projects, ensure the startup project is set correctly in Visual Studio:

1. Right-click the project that contains `Program.cs` (usually your Web project)
2. Click Set as Startup Project
3. Make sure Default Project in Package Manager Console is also the same project.

---

4. **Install EF Core Tools (if not installed)**
```bash
dotnet tool install --global dotnet-ef
```

5. **Run Migrations**

If someone clones the repository, they can create or update the database using migrations:

Using Package Manager Console (Visual Studio):
1. Open Package Manager (PM) Console
2. Select the default project to `RetailShopManagement.Application`
3. Run the migration commands:

- **Apply all pending migrations**

**PM Console:**
```bash
Update-Database
```
**Using .NET CLI:**
Ensure you're in the startup project folder
```bas
dotnet ef database update
```
- **This will create the database if it does not exist and apply all migrations.**
