## Hospital Management System API 🏥
This project is a robust and secure RESTful API built with the latest ASP.NET 9 framework. The Hospital Management System provides a complete backend solution for managing hospital operations, including patient data, appointments, and staff interactions, with a strong focus on security and role-based access control.

## Key Features
Secure Authentication & Authorization: The API is protected using JSON Web Tokens (JWT), ensuring that only authenticated users can access the protected endpoints.

Role-Based Access Control: The system implements a granular permissions model with four distinct roles, restricting access to specific functionalities based on user roles:

Admin: Full access to manage all system settings and user accounts.

Doctor: Access to view patient records, manage appointments, and write prescriptions.

Receptionist: Manages patient registration, schedules appointments, and handles billing.

Technician: Access to lab results and medical equipment records.

Modern API Architecture: Built following RESTful principles for a predictable and scalable API.

Efficient Data Handling: Utilizes Entity Framework Core for seamless and efficient communication with the database.

## Technology Stack
Framework: ASP.NET 9 Web API

Database: Microsoft SQL Server

ORM: Entity Framework Core

Authentication: JWT (JSON Web Tokens)
