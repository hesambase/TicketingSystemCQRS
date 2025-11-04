\# 🎟️ Ticketing System (SQLite Version - Prebuilt DB)

\## 🚀 How to Run the Project

1\. \*\*Clone the repository\*\*

\`\`\`bash

git clone &lt;repo-url&gt;

cd TicketingSystem

- **Database already included**
  - The SQLite database file (TicketingSystem.db) is shipped with the project.
  - There is no need to run dotnet ef database update or create new migrations.
  - Tables and initial data (Admin and Employee users) are already prepared.
- **Run the API**
- dotnet run --project TicketingSystem.Api
  - The API will be available at <https://localhost:5001> or <http://localhost:5000>.
  - Swagger UI is available at /swagger.

**🌱 Initial Users (Seeded Data)**

The prebuilt database includes two initial users:

- **Admin User**
  - Email: <admin@example.com>
  - Password: Admin123!
  - Role: Admin
- **Employee User**
  - Email: <employee@example.com>
  - Password: Employee123!
  - Role: Employee

Passwords are hashed using **BCrypt.Net**.  
If the database already exists, seeding will not run again.

**📌 Assumptions and Decisions**

- **Database**: SQLite is used, and the database file (TicketingSystem.db) is provided with the project.
- **Architecture**: The project follows Clean Architecture principles:
  - Domain: Entities and Enums
  - Application: Commands/Queries/Handlers
  - Infrastructure: EF Core DbContext and Persistence
  - Api: Web API and Controllers
- **Authentication/Authorization**:
  - Roles (Admin, Employee) are embedded in JWT tokens.
  - Endpoints are restricted with \[Authorize(Roles = "...")\].
- **Ticket Lifecycle**:
  - Tickets can only be created by Employee.
  - All tickets can be viewed and updated/assigned only by Admin.
  - Each employee can only view their own tickets.
- **Passwords**: Always stored as hashed values (using BCrypt.Net-Next).
- **Migrations**: End users do not need to run migrations, since the database is prebuilt and included.