# CleanUsers API

## Description

This repository is currently a work in progress where I can consolidate my learnings into a sample repository.

This is similar to [Users API](https://github.com/darrenleeyx/Users) but with additional architectural and design patterns such as
1. Clean Architecture
2. CQRS (Command Query Responsibility Segregation)
3. Unit of Work & Repository Pattern with EntityFrameworkCore (Sqlite)
4. Results Pattern

## Projects
Architecture of CleanUsers API
![CleanUsers API Architecture](https://github.com/darrenleeyx/CleanUsers/assets/59044608/6d3bf2a2-9442-4ff1-ac5e-7eb5b1d3c64a)

1. src/CleanUsers.Api
2. src/CleanUsers.Api.Contracts
3. src/CleanUsers.Application
4. src/CleanUsers.Domain
5. src/CleanUsers.Infrastructure

## Features



## EntityFrameworkCore

* Add Migration
```console
PM> Add-Migration InitialCreate -p CleanUsers.Infrastructure -s CleanUsers.Api
```

* Update Database
```console
PM> Update-Database
```
