# Invoice System

A RESTful API for managing invoices, allowing creation, payment, and processing of overdue invoices.

# Project Architecture

This project follows a **3-tier architecture**, ensuring separation of concerns for better scalability and maintainability:

### 1. Invoice.API
- Handles HTTP requests and responses.
- Contains:
  - Controllers
  - Mappings
  - Configuration files for the API.
 
### 2. Invoice
- Implements business logic.
- Contains:
  - **Services**: Core logic for invoice processing.
  - **Enums**: Enumerations for statuses and other constants.
  - **Models**: Domain models used for core operations.

### 3. Invoice.Data
- Manages database operations and domain models.
- Contains:
  - **Entities**: Database entity definitions.
  - **DTOs**: Data Transfer Objects for input/output operations.
  - **Repository Pattern**: Encapsulates data access logic.
  - **Database Context (`InvoiceDbContext`)**: Manages EF Core interactions with the database.

### 4. Invoice.Test
- Contains unit tests for domain logic and services.
- Ensures functionality and correctness of the system.


## Features

- **Create Invoice**:  
  `POST /invoices`  
  Allows the creation of a new invoice.

- **View Invoices**:  
  `GET /invoices`  
  Retrieve a list of all invoices.

- **Make Payment**:  
  `POST /invoices/{id}/payments`  
  Process a payment for a specific invoice by its ID.

- **Process Overdue Invoices**:  
  `POST /invoices/process-overdue`  
  Automatically process overdue invoices, applying late fees and generating new invoices for unpaid amounts if necessary.

## How to Run

### Clone the Repository
```bash
git clone https://github.com/ananya-egdk/Invoice-System.git
cd Invoice-System
