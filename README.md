# 🏋️‍♂️ Gym Management System
### Enterprise-Grade ASP.NET Core MVC Application

> A **production-ready Gym Management System** built with **ASP.NET Core MVC**, following **clean architecture**, enforcing **real-world business rules**, and designed for **scalability and maintainability**.

---

## 🚀 Highlights

- ✅ Clean Architecture
- ✅ Real Business Logic Enforcement
- ✅ Identity & Role-Based Authentication
- ✅ Database Constraints & Validations
- ✅ Enterprise-Ready Structure

---

## 📖 Table of Contents

- [Project Vision](#-project-vision)
- [System Capabilities](#-system-capabilities)
- [Application Architecture](#-application-architecture)
- [Technology Stack](#-technology-stack)
- [Core Domain Model](#-core-domain-model)
- [Business Rules Engine](#-business-rules-engine)
- [MVC Structure](#-mvc-structure)
- [Identity & Security](#-identity--security)
- [Database Design](#-database-design)
- [Frontend Repository](#-frontend-repository)
- [Getting Started](#-getting-started)
- [Quality & Best Practices](#-quality--best-practices)
- [Future Enhancements](#-future-enhancements)
- [Author](#-author)

---

## 🎯 Project Vision

Modern gyms require **automation, accuracy, and control**.

This project aims to:
- Centralize gym operations
- Enforce real-world business constraints
- Ensure data consistency
- Support future scalability

Built as an **enterprise-style system**, not a simple CRUD demo.

---

## ⚙️ System Capabilities

### 👤 Member Management
- Full CRUD operations
- Mandatory health record on registration
- Egyptian phone validation
- Profile photo support
- Prevent deletion with active bookings

### 🏋️ Trainer Management
- Specialty-based trainers
- Automatic hire date assignment
- Prevent deletion with future sessions

### 📅 Session Management
- Capacity enforcement (1–25)
- Trainer & category assignment
- Date validation (EndDate > StartDate)
- Prevent deletion of future sessions

### 🎟️ Booking System
- Active membership required
- Capacity checks
- No duplicate bookings
- Attendance tracking
- Future-only cancellation

### 💳 Membership & Plans
- Single active membership per member
- Auto-calculated expiration
- Soft delete plans
- Active-plan enforcement

### 📊 Dashboard
- Analytics & reporting overview

---

## 🏗️ Application Architecture

### Three-Layer Architecture

## Presentation Layer
├─ ASP.NET MVC Controllers
├─ Razor Views
├─ Bootstrap & Custom CSS
│
## Business Logic Layer
├─ Services
├─ Domain Rules
## Data Access Layer
├─ Entity Framework Core
├─ Repository Pattern
├─ Unit of Work


---

## 🧰 Technology Stack

| Layer | Technology |
|------|-----------|
| Backend | ASP.NET Core MVC |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Frontend | Razor Views, Bootstrap |
| Authentication | ASP.NET Identity |
| Patterns | Repository, Unit of Work |
| Mapping | AutoMapper |

---

## 🧩 Core Domain Model

### Main Entities
- Member
- Trainer
- Plan
- Session
- Category

### Supporting Entities
- HealthRecord
- Booking (Member ↔ Session)
- Membership (Member ↔ Plan)

### Inheritance
- GymUser (Abstract Base Class)
  - Shared personal and address data
  - Extended by Member and Trainer

---

## 📜 Business Rules Engine

### Booking Rules
- Member must have an active membership
- Session must have available capacity
- No duplicate bookings allowed
- Only future sessions can be booked
- Attendance allowed only for ongoing sessions

### Membership Rules
- Only one active membership per member
- End date calculated automatically
- Only active plans can be assigned
- Membership status computed dynamically

All rules are enforced at **application and database levels**.

---

## 🧠 MVC Structure

### Controllers
- HomeController
- MemberController
- TrainerController
- SessionController
- PlanController
- MemberPlanController
- MemberSessionController
- AccountController

### Views
- Razor-based UI
- Bootstrap responsive layout
- Reusable components

---

## 🔐 Identity & Security

- ASP.NET Identity
- Role-based authorization
- Secure login & logout
- Access denied handling
- Scalable role management

---

## 🗄️ Database Design

- SQL Server
- Strong relational constraints
- Soft deletes
- Junction tables for many-to-many relations

## 🧪 Quality & Best Practices

- SOLID Principles  
- Clean Architecture  
- Dependency Injection  
- DRY & reusable services  
- Production-ready structure  

---

## 👨‍💻 Author

**Nour Saber**  
ASP.NET Backend Developer  






