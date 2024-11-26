# Activity Calendar Web Application

Activity Calendar Web Application is a modern web application developed using ASP.Net Core and EF Core, designed to manage calendar activities. The application provides users with a convenient interface for viewing and managing activities.

## Features

- CRUD Operations: Allows creation, reading, updating, and deletion of activities.
- Client-Side Integration: Implements the client-side using React to provide an interactive and responsive user interface.
- Clean Architecture: Easy to update and add new functions.

## Tech Stack

**Client:** React

**Server:** ASP.Net Web Api, EF Core, PostgreSQL

## Installation

Change *DefaultConnection* of your database in **appsettings.json**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "{your connection string}"
  },
}
```

## Deployment

To update the database run

```bash
dotnet ef database update -p .\ActivityCalendarWebApp.Persistence -s .\ActivityCalendarWebApp.API
```
