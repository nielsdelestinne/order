# Example backend Örder

- Example backend for a project called Örder
- ASP.NET Core 2.1.X
- EF Core 2.1.X

## Usage

1. Start the backend (e.g. with IIS Express), it will open up the Swagger UI
2. Using the Swagger UI, inspect the allowed calls and required models (DTO objects)

## Remarks

- EF Core is configured to use an in-memory database. Setting it up with a different database provider should be fairly simple.
- This is by no means a production ready application (HTTPS not configured, not thouroughly, nor properly, tested)
- The purpose of this backend is to use it as-is, as an example backend to which you can develop a frontend.
- Currently, no authentication or authorization is configured. 

