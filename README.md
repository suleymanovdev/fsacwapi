# FSACWAPI - .NET Web API Project

A .NET Web API project featuring user authentication, JWT token-based authorization, and Google Cloud Storage integration for profile photo management.

## Features

- User authentication and registration
- JWT token-based authorization
- Role-based access control
- Profile photo management using Google Cloud Storage
- PostgreSQL database integration
- Swagger API documentation
- Fluent Validation for request validation
- Result Pattern integration
- In-memory caching

## Prerequisites

- .NET 8.0 or later
- PostgreSQL database
- Google Cloud Storage account and credentials
- Development environment (Rider, VS Code, or similar)

## Configuration

Google Cloud Storage
   - Place your Google Cloud service account key file (`.json`) in the project root
   - Update the `ProjectId` and `ServiceAccountKeyFilePath` in `StorageService.cs` if needed

## Getting Started

1. Clone the repository
2. Install dependencies:
   ```bash
   dotnet restore
   ```
3. Run the application:
   ```bash
   dotnet run
   ```

## API Endpoints

### Authentication

- `POST /api/Auth/register` - Register a new user
- `POST /api/Auth/login` - Login and receive JWT token
- `GET /api/Auth/user-data/{email}` - Get user data (requires authentication)

## Security

The API uses:
- JWT Bearer token authentication
- Role-based authorization

## Documentation

API documentation is available through Swagger UI when running in development mode:
- Access Swagger UI at: `https://localhost:{port}/swagger`
- Detailed API documentation with request/response schemas
- Built-in API testing interface

## Error Handling

The API implements:
- Global exception handling
- Validation error responses
- HTTP status codes for different scenarios
- Structured error messages

## Development

To run in development mode:
1. Swagger UI will be automatically available
2. Detailed error messages will be shown

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

[https://github.com/suleymanovdev/fsacwapi/blob/main/LICENSE]

by sudev.
