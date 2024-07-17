# PBCW2 Project

This project is a simple ASP.NET Core API application developed during Papara Bootcamp Week 2.

## Features

- CRUD operations for Product model
- Listing and sorting products
- Validation using FluentValidation
- Error handling with standard HTTP status codes
- Custom authorization for specific endpoints
- Swagger integration with custom headers for specific endpoints

## Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/ramazanefegurkan/PBCW2.git
    cd PBCW2
    ```

2. Install the dependencies:
    ```bash
    dotnet restore
    ```

3. Run the application:
    ```bash
    dotnet run
    ```

## Usage

### API Endpoints

- **List Products:** `GET /api/products/list`
- **Sort Products by Price:** `GET /api/products/sortByPrice`
- **Get Product:** `GET /api/products/{id}`
- **Create Product:** `POST /api/products`
- **Update Product:** `PUT /api/products/{id}`
- **Delete Product:** `DELETE /api/products/{id}`
- **Secure Data Endpoint:** `GET /api/products/secure-data` (Requires `Token` header)

## Custom Authorization Middleware

A custom middleware has been added to handle authorization for specific endpoints.

## Swagger Integration

Swagger has been integrated to provide API documentation and testing interface. Custom headers for the `secure-data` endpoint have been added in Swagger using `OperationFilter`.
