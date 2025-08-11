# DotNetCoreApiBasics
A simple ASP.NET Core Web API project applying core API concepts, authentication, and documentation.

# ASP.NET Core API Fundamentals Project

This is a simple **ASP.NET Core Web API** project created to **demonstrate and explore the fundamentals of building APIs** with authentication, authorization, and documentation using real implementations and examples.

---

## What’s Implemented in This Project

### 1. **API Fundamentals**
- Implemented RESTful endpoints with standard HTTP verbs (`GET`, `POST`, `PUT`, `DELETE`).
- Layered architecture separating controllers, services, and data access.

### 2. **Authentication & Authorization**
- **ASP.NET Core Identity** for user management.
- **JWT (JSON Web Token)** for secure authentication.
- Role-based authorization for endpoint access control.

### 3. **Swagger API Documentation**
- Integrated Swagger for easy API exploration and testing.
- Detailed endpoint descriptions with request/response models.

### 4. **Entity Framework Core Integration**
- Database connection using EF Core with Code-First approach.
- Migrations and seeding for initial data.

---

## Technologies Used
- ASP.NET Core Web API
- Entity Framework Core
- ASP.NET Core Identity
- JWT (JSON Web Token)
- Swagger / Swashbuckle
- C#
- SQL Server

---

## Project Structure
- `Controllers/` → API endpoints
- `Models/` → Data models
- `Data/` → Database context and migrations
- `Services/` → Business logic
- `Configurations/` → JWT and Identity setup
- `Program.cs` → Application configuration and middleware

---

## How to Run
```bash
# Clone the repository
git clone https://github.com/MuhammedReda263/DotNetCoreApiBasics.git
cd DotNetCoreApiBasics

# Update the connection string in appsettings.json

# Apply migrations
dotnet ef database update

# Run the application
dotnet run
