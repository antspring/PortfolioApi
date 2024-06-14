# Portfolio API

Portfolio API - is a web service that provides an API for managing a user's portfolio.

## Requirements

- .NET 8.0.2
- PostgreSQL 16.3.1

## Installation

### Clone the repository:

```bash
git clone https://github.com/antspring/PortfolioApi.git
```

### Go to directory

```bash
cd Portfolio
```

## Run the application

### With Docker

#### Copy the .env file from .env.example and fill it out 

#### Start Docker Containers

```bash
docker-compose up
```

### Without Docker

#### Copy the .env file from .env.example and fill it out

#### Install Dependencies

```bash
dotnet restore
```

#### Start the project

```bash
dotnet run
```