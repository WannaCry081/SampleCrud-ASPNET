# 🛠️ Sample CRUD Application [ASP.NET]

This project demonstrates a CRUD (Create, Read, Update, Delete) application developed using .NET 8 in ASP.NET Core WebAPI. It showcases basic CRUD operations and provides a template for building RESTful APIs with ASP.NET Core.

## 🚀 Features

- **CRUD Operations**: Perform Create, Read, Update, and Delete operations on entities.
- **Entity Framework Core**: Utilizes EF Core for database interactions.
- **Swagger**: Integrated Swagger for API documentation and testing.
- **Dependency Injection**: Follows best practices for dependency injection.
- **Error Handling**: Implements global error handling and logging.
- **User Authentication**: Secure user authentication with endpoints for login and registration.
- **User Information**: Retrieve authenticated user information using `/users/me`.

## 🛠️ Technologies

- **GIT**: Version control system
- **.NET 8**: Latest .NET framework version
- **ASP.NET Core WebAPI**: API framework for building web applications
- **Entity Framework Core**: ORM for database interactions
- **Swagger**: API documentation tool

## ⚙️ Installation

1. Clone the repository:

  ```bash
  git clone https://github.com/WannaCry081/SampleCrud-ASPNET.git
  ```

2. Update the connection string in `appsettings.json` to match your configuration:

  ```json
  {
    "ConnectionStrings": {
      "DefaultConnection": "Your Connection String Here"
    }
  }
  ```

3. Build and run the program:

  ```bash
  dotnet build   # Builds the application
  dotnet run     # Runs the application
  ```

4. Alternatively, if Docker is installed on your system, you can start the application with Docker Compose. Ensure you have a `.env` file in the project directory for configuring the environment variables.

  ```bash
  docker compose --env-file=./.env up
  ```

The application should now be running at [http://localhost:8080/](http://localhost:8080/swagger/index.html)

5. Create a `.env` file in the root directory to configure environment variables for `docker-compose.yml`. Below is a sample template for your `.env` or check out the `.env.example` file:

```
# Application Base URL
APPLICATION_URL="http://localhost:8080"

# Database connection string
DEFAULT_CONNECTION="YourDatabaseConnectionStringHere"
```

## 📖 Usage

After starting the application, you can access the following features:

- **Create Entity**: Add a new entity by sending a POST request to `/api/v1/notes` with the necessary data.
- **Read Entities**: Retrieve a list of entities by sending a GET request to `/api/v1/notes`.
- **Read Entity by ID**: Retrieve a specific entity by sending a GET request to `/api/v1/notes/{id}`.
- **Update Entity**: Update an existing entity by sending a PUT request to `/api/v1/notes/{id}` with the updated data.
- **Delete Entity**: Delete an entity by sending a DELETE request to `/api/v1/notes/{id}`.
- **User Registration**: Register a new user by sending a POST request to `/register` with the necessary data.
- **User Login**: Authenticate a user by sending a POST request to `/login` with the necessary credentials.
- **Get User Info**: Retrieve authenticated user information by sending a GET request to `/users/me`.

Each endpoint ensures secure handling of data and follows RESTful principles.

## 🤝 Contributing

If you'd like to contribute to this project, please follow these guidelines:

1. Fork the repository.
2. Create a new branch for your feature:

```bash
git checkout -b feature/YourFeature
```

3. Commit your changes:

```bash
git commit -am 'feat: add new feature'
```

4. Push to the branch:

```bash
git push origin feature/YourFeature
```

5. Create a new Pull Request for review.

> [!NOTE]
> Ensure that all commits follow the [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0-beta.2/) specification for consistent and meaningful commit messages.

## 📬 Contact

For any questions or feedback, feel free to reach out:

- **Email:** liraedata59@gmail.com
- **GitHub:** [WannaCry081](https://github.com/WannaCry081)
