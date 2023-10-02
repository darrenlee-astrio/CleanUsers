# CleanUsers API

## Description

This is similar to [Users API](https://github.com/darrenleeyx/Users) but with additional architectural and design patterns such as
1. Clean Architecture
2. CQRS (Command Query Responsibility Segregation)
3. Unit of Work & Repository Pattern with EntityFrameworkCore (Sqlite)
4. Results Pattern

## Projects


## Features



## EntityFrameworkCore

* Add Migration
Add-Migration InitialCreate -p CleanUsers.Infrastructure -s CleanUsers.Api

* Update Database
Update-Database