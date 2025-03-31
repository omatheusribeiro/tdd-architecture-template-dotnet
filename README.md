# 📌 TDD Architecture .NET 8 API

## 📚 Overview
The **TDD Architecture .NET 8 API** is a structured and well-organized example of implementing TDD Architecture using **.NET Core 8**. This project serves as a learning reference for developers who want to understand and implement TDD Architecture using best practices and modern development patterns. 🚀

This repository demonstrates:

👉 **Separation of Concerns (SoC) through TDD Architecture** 🏰  
👉 **Implementation of Code First with Migrations** 🛂  
👉 **Usage of Repository Pattern, Unit of Work and Singleton** 🔄  
👉 **Automated Testing with xUnit** 🧪  
👉 **Authentication and Authorization with JWT** 🔑   

## 🏰 Project Structure
```
tdd-architecture-dotnet.Api
 ├── Config              # Configuration files, such as AutoMapper settings
 │   ├── MappingConfig   # Mapping profiles for AutoMapper
 ├── Controllers         # API controllers handling HTTP requests
 │   ├── V1              # Versioned API controllers
 ├── Middlewares         # Custom middleware components
 ├── Properties          # Metadata and project properties

tdd-architecture-dotnet.Application
 ├── Mappings           # AutoMapper configuration for domain-to-view model mappings
 ├── Models             # Application-level data models
 │   ├── Http          # HTTP request/response models
 ├── Services           # Business logic and service layer
 │   ├── Login         # Authentication and authorization logic
 │   │   ├── Interfaces # Interfaces for login services
 │   ├── Products      # Product-related business logic
 │   │   ├── Interfaces # Interfaces for product services
 │   ├── Sales         # Sales-related business logic
 │   │   ├── Interfaces # Interfaces for sales services
 │   ├── Users         # User-related business logic
 │   │   ├── Interfaces # Interfaces for user services
 ├── ViewModels         # View models for API responses
 │   ├── Products      # Product-related view models
 │   ├── Sales         # Sales-related view models
 │   ├── Users         # User-related view models

tdd-architecture-dotnet.Domain
 ├── Entities           # Core domain entities
 │   ├── Base          # Base classes for domain entities
 │   ├── Products      # Product entity definitions
 │   ├── Sales         # Sales entity definitions
 │   ├── Users         # User entity definitions
 ├── Enums             # Enumeration types for the domain layer
 ├── Interfaces        # Repository interfaces

tdd-architecture-dotnet.Infrastructure
 ├── Authentication      # Authentication implementation (JWT, token management)
 ├── Data                # Data access layer
 │   ├── Context         # Database context configuration (Entity Framework Core)
 │   ├── EntitiesConfiguration # Database table configurations (EF Core Fluent API)
 │   │   ├── Products      # Product table configurations
 │   │   ├── Sales         # Sales table configurations
 │   │   ├── Users         # User table configurations
 ├── Repositories       # Data access layer
 │   ├── Products      # Product repository implementations
 │   ├── Sales         # Sales repository implementations
 │   ├── Users         # User repository implementations
 ├── Singletons       # Global Sharing Layer
 │   ├── Cache  # Cache Singleton
 │   │   ├── Interfaces      # Cache Singleton Interface
 │   ├── Logger  # Logger Singleton
 │   │   ├── Interfaces      # Logger Singleton Interface

tdd-architecture-dotnet.Tests
 ├── Controller       # Apresentation layer testing
 │   ├── Products      # Product tests implementations
 │   ├── Sales         # Sales tests implementations
 │   ├── Users         # User tests implementations
 ├── Services       # Application layer testing
 │   ├── Products      # Product tests implementations
 │   ├── Sales         # Sales tests implementations
 │   ├── Users         # User tests implementations
 ├── Services       # Infrastructure layer testing
 │   ├── Products      # Product tests implementations
 │   ├── Sales         # Sales tests implementations
 │   ├── Users         # User tests implementations
```

## 🚀 Getting Started

### 📝 Prerequisites
Make sure you have the following installed:
- [.NET Core 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Postman](https://www.postman.com/) (optional, for API testing)

### 🔧 Installation
```bash
# Clone the repository
git clone https://github.com/omatheusribeiro/tdd-architecture-template-dotnet.git
cd tdd-architecture-template-dotnet
```

### ⚙️ Configuration
Before running the application, configure the **database connection string** in:
- `appsettings.json`
- `appsettings.Development.json`

Example:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=localhost;Initial Catalog=tdd-architecture-dotnet;Integrated Security=True;TrustServerCertificate=True"
}
```

### ▶️ Running the Application
```bash
# Start the API
dotnet run --project tdd-architecture-dotnet.Api
```
The application will automatically check if the database and tables exist. If not, they will be created upon execution.

## 🔑 Authentication & Initial Token Usage
To generate an authentication token, use the following credentials in the **login endpoint**:
- **Email:** `usertest@test.com.br`

## 🛠️ Features & Modules
This application includes:

👉 **User Registration (With Address & Contact Info)** 👤  
👉 **Product Management (With Product Types)** 🛂  
👉 **Sales Registration (With Business Rules Applied)** 💰  

## 🛠️ Technologies Used
- **.NET Core 8** 🚀
- **Entity Framework Core (Code First + Migrations)** 🏰
- **SQL Server** 📂
- **AutoMapper** 🔄
- **xUnit (Unit Testing)** 🧪
- **JWT Authentication** 🔑
- **Repository & Service Layer Pattern** 📚

## 📄 License
This project is licensed under the BSD-2 Clause License.

