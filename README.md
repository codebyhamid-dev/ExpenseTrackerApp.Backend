**ExpenseTrackerApp.Backend**

An Expense Tracker backend application built with ASP.NET Core Web API that enables users to securely manage their income and expenses.
The system provides robust user management, JWT-based authentication, and transaction handling, following modern backend development best practices and clean architectural principles.

**🚀 Features**

User registration and authentication

Secure access using JWT-based authentication

User management powered by ASP.NET Core Identity

Create, update, retrieve, and delete expense and income transactions

Support for both income and expense tracking

Transaction categorization for better financial organization

User-specific data access ensuring data isolation and security

Clean separation of concerns using a layered architecture

API-first design, ready for integration with frontend applications

**🛠️ Tech Stack**

Domain-Driven Design (DDD)

ASP.NET Core Web API

ASP.NET Core Identity

JSON Web Tokens (JWT)

Entity Framework Core

LINQ

SQL Server

AutoMapper

Repository Pattern

Swagger / OpenAPI for API documentation

**📂 Project Structure
**
The solution follows a clean, layered architecture inspired by Domain-Driven Design (DDD) principles.
Each layer has a clear responsibility, improving maintainability, scalability, and testability of the application.
This structure ensures that business logic, data access, contracts, and API concerns remain properly separated.

<img width="306" height="577" alt="image" src="https://github.com/user-attachments/assets/35375653-fe7d-4f35-a715-ee88f700f5b1" />

🔗 API Endpoints / Controllers

The application exposes RESTful endpoints through dedicated controllers, handling authentication and transaction-related operations.
All secured endpoints require JWT authentication, ensuring only authorized users can access protected resources.

<img width="717" height="527" alt="image" src="https://github.com/user-attachments/assets/60740872-fa6e-43c5-8937-190cb82c8827" />
<img width="718" height="227" alt="image" src="https://github.com/user-attachments/assets/783bd011-f178-4c5c-ae3b-d5d6f1173ad5" />
<img width="718" height="576" alt="image" src="https://github.com/user-attachments/assets/c867e740-07d7-4c1c-9b57-2a53e8efbebc" />
<img width="714" height="360" alt="image" src="https://github.com/user-attachments/assets/72e64bf2-ecd5-4e93-ace9-edbd7903d680" />

The solution follows a clean, layered architecture to keep concerns separated and the codebase maintainable.
