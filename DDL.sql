--1️⃣  Roles Table

CREATE TABLE Roles (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(255),
    CreatedAt DATETIME2 DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NULL
);


-- 2️⃣  Permissions Table

CREATE TABLE Permissions (
    PermissionId INT IDENTITY(1,1) PRIMARY KEY,
    PermissionName NVARCHAR(150) NOT NULL UNIQUE,
    Description NVARCHAR(255),
    CreatedAt DATETIME2 DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NULL
);


-- 3️⃣  RolePermissions Table

CREATE TABLE RolePermissions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RoleId INT NOT NULL,
    PermissionId INT NOT NULL,
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId),
    FOREIGN KEY (PermissionId) REFERENCES Permissions(PermissionId)
);


-- 4️⃣  Users Table

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(500) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    FullName NVARCHAR(255),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME2 DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NULL
);


-- 5️⃣  UserRoles Table

CREATE TABLE UserRoles (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);


-- 6️⃣  RefreshTokens Table

CREATE TABLE RefreshTokens (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    TokenHash NVARCHAR(500) NOT NULL,
    ExpiresAt DATETIME2 NOT NULL,
    IsRevoked BIT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT SYSUTCDATETIME(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);


-- 7️⃣  Banks Table

CREATE TABLE Banks (
    BankId INT IDENTITY(1,1) PRIMARY KEY,
    BankName NVARCHAR(255) NOT NULL,
    BankCode NVARCHAR(50) UNIQUE NOT NULL,
    CreatedAt DATETIME2 DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NULL
);


-- 8️⃣  Branches Table

CREATE TABLE Branches (
    BranchId INT IDENTITY(1,1) PRIMARY KEY,
    BankId INT NOT NULL,
    BranchName NVARCHAR(255) NOT NULL,
    BranchCode NVARCHAR(50) UNIQUE NOT NULL,
    Address NVARCHAR(255),
    CreatedAt DATETIME2 DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NULL,
    FOREIGN KEY (BankId) REFERENCES Banks(BankId)
);


-- 9️⃣  Accounts Table

CREATE TABLE Accounts (
    AccountId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    BankId INT NOT NULL,
    BranchId INT NULL,
    AccountNumber NVARCHAR(50) UNIQUE NOT NULL,
    AccountType NVARCHAR(50) NOT NULL,
    Currency NVARCHAR(10) DEFAULT 'INR',
    Balance DECIMAL(18,2) DEFAULT 0,
    IsMinor BIT DEFAULT 0,
    IsPOA BIT DEFAULT 0,
    CreatedAt DATETIME2 DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIME2 NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (BankId) REFERENCES Banks(BankId),
    FOREIGN KEY (BranchId) REFERENCES Branches(BranchId)
);


--   AccountOperations Table

CREATE TABLE AccountOperations (
    OperationId INT IDENTITY(1,1) PRIMARY KEY,
    AccountId INT NOT NULL,
    OperationType NVARCHAR(50) NOT NULL,
    Amount DECIMAL(18,2) NULL,
    PerformedBy INT NOT NULL,
    PerformedAt DATETIME2 DEFAULT SYSUTCDATETIME(),
    FOREIGN KEY (AccountId) REFERENCES Accounts(AccountId),
    FOREIGN KEY (PerformedBy) REFERENCES Users(UserId)
);


-- 1️⃣1️⃣  Employees Table

CREATE TABLE Employees (
    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    BankId INT NOT NULL,
    BranchId INT NULL,
    RoleTitle NVARCHAR(100),
    CreatedAt DATETIME2 DEFAULT SYSUTCDATETIME(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (BankId) REFERENCES Banks(BankId),
    FOREIGN KEY (BranchId) REFERENCES Branches(BranchId)
);


-- 1️⃣2️⃣  Indexes

CREATE INDEX IX_UserRoles_UserId ON UserRoles(UserId);
CREATE INDEX IX_RefreshTokens_UserId ON RefreshTokens(UserId);
CREATE INDEX IX_Branches_BankId ON Branches(BankId);
CREATE INDEX IX_Accounts_UserId ON Accounts(UserId);
CREATE INDEX IX_Accounts_BankId ON Accounts(BankId);
CREATE INDEX IX_Employees_BankId ON Employees(BankId);
