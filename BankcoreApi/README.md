# 🏦 BankCoreApi

BankCoreApi is a simple backend system for managing users, banks, branches, and accounts using **ASP.NET Core Web API**, **Entity Framework Core**, and **SQL Server**.

---

## 🚀 Features

- User management (create, update, delete, read)
- Bank and branch management
- Account creation (savings/current)
- Deposit, withdraw, and close accounts
- Role-based access (roles and permissions)
- Audit timestamps for all records
- Swagger API documentation

---

## ⚙️ Technologies Used

- ASP.NET Core 8.0
- Entity Framework Core
- Microsoft SQL Server
- C#
- Swagger / OpenAPI

---

## 📁 Project Structure

```
BankCoreApi/
│
├── Controllers/       -> API controllers (Users, Accounts, Banks, etc.)
├── Models/            -> Entity models
├── DTOs/              -> Data Transfer Objects
├── Data/              -> Database context and configuration
├── Migrations/        -> EF Core migrations
├── Program.cs         -> Application entry point
├── appsettings.json   -> Configuration file
└── README.md
```

---

## 🧩 Database Overview

**Main Tables**

- Users  
- Roles  
- Permissions  
- UserRoles  
- RolePermissions  
- Banks  
- Branches  
- Accounts  
- AccountOperations  
- Employees  
- RefreshTokens  

Each table includes `CreatedAt` and `UpdatedAt` fields for tracking changes.

---

## ⚙️ Setup Instructions

### 1. Clone the Project
```bash
git clone https://github.com/<your-username>/BankCoreApi.git
cd BankCoreApi
```

### 2. Update Database Connection
Edit your **appsettings.json** file:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=BankCoreDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3. Install Dependencies
```bash
dotnet restore
```

### 4. Apply Migrations
```bash
dotnet ef database update
```

### 5. Run the Project
```bash
dotnet run
```

### 6. Open Swagger UI
Open your browser and go to:
```
https://localhost:5001/swagger
```

---

## 🧰 Dependencies

- Microsoft.EntityFrameworkCore  
- Microsoft.EntityFrameworkCore.SqlServer  
- Microsoft.AspNetCore.Authentication.JwtBearer  
- Swashbuckle.AspNetCore  

---

## 🧠 Future Improvements

- Add password encryption  
- Add email verification  
- Add admin dashboard  
- Add transaction history and reports  

---




