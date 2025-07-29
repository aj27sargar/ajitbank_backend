# 🏦 AjitBank_Backend

A secure and modular **ASP.NET Core MVC backend** for user authentication in a banking system. It supports **Login**, **Registration**, **Logging**, and interacts with a **SQL Server** database using **Stored Procedures**.

> 🔐 Designed with a focus on clean architecture, scalability, and database performance.

---

## 📌 Features

- 🧑‍💻 User Registration with password hashing
- 🔐 Login with JWT token-based authentication (optional)
- 📊 SQL Server integration via **ADO.NET**
- 🛢️ Uses **Stored Procedures** for secure DB operations
- 🧾 Logs all user actions (success/failures) into log files
- 📦 Scalable folder structure with **MVC pattern**

---

## 🧱 Tech Stack

| Layer       | Technology        |
|------------|--------------------|
| Backend    | ASP.NET Core (.NET 6/7) |
| Architecture | MVC (Model-View-Controller) |
| Database   | SQL Server (SSMS) |
| DB Access  | ADO.NET + Stored Procedures |
| Logging    | Built-in ILogger / Custom Log File |
| Auth       | JWT Token (if implemented) |

---

## 🗂️ Project Structure

```

AjitBank\_Backend/
├── Controllers/
│   └── AuthController.cs
├── Models/
│   └── User.cs
├── Services/
│   └── AuthService.cs
│   └── JwtService.cs (optional)
├── DAL/
│   └── DatabaseHelper.cs
├── Logs/
│   └── activity-log.txt
├── appsettings.json
├── Program.cs
├── Startup.cs
└── AjitBankDB (SSMS DB)

````

---

## 🛢️ Database (SQL Server)

### ✅ Stored Procedures Used:

- `sp_RegisterUser`
- `sp_LoginUser`
- `sp_CheckUserExists`

All queries are parameterized to prevent SQL injection.

---

## 🚀 How to Run

### 🖥️ Prerequisites

- [.NET SDK 6 or 7](https://dotnet.microsoft.com/)
- [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/)
- Visual Studio or VS Code

### 🔧 Setup Steps

1. **Clone the repository**:
 ```bash
   git clone https://github.com/aj27sargar/ajitbank_backend.git
   cd ajitbank_backend
````

2. **Update `appsettings.json`** with your local SQL Server connection string:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=AjitBankDB;Trusted_Connection=True;"
   }
   ```

3. **Run the application**:

   ```bash
   dotnet run
   ```

4. **Use Swagger or Postman** to test:

   * `POST /api/auth/register`
   * `POST /api/auth/login`

---

## 📄 API Endpoints

| Endpoint             | Method | Description        |
| -------------------- | ------ | ------------------ |
| `/api/auth/register` | POST   | Registers new user |
| `/api/auth/login`    | POST   | Logs in user       |

Request format:

```json
{
  "username": "ajit",
  "password": "123456"
}
```

---

## 📜 Logs

All actions (success or failure) are saved into:

```
/Logs/activity-log.txt
```

Each log includes timestamp, action type, username, and result.

---

## 🧩 Optional Improvements

* Add **JWT token authentication**
* Add **User Profile Update API**
* Add **Role-based Access Control (RBAC)**
* Enable **CORS** for frontend (React/Angular)

---

## 🙋‍♂️ Author

**Ajit Sargar**
📧 [ajitsargar@kccemsr.edu.in](mailto:ajitsargar@kccemsr.edu.in)
🔗 [GitHub](https://github.com/aj27sargar) | [LinkedIn](https://www.linkedin.com/in/ajit-sargar-495a1a253/)

---

## 🪪 License

This project is licensed under the [MIT License](./LICENSE)

```
