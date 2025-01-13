# Invoice System

A RESTful API for managing invoices, allowing creation, payment, and processing of overdue invoices.

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
