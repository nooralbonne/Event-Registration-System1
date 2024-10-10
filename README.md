# Event Registration System

## Overview

This project is an **Event Registration System** built using ASP.NET Core MVC. The system allows users to view events, register for them, and receive email notifications. It is designed to simplify event management and enhance user experience, supporting full registration functionality.

## Features

- View a list of events
- Register for events
- Receive email notifications upon registration
- User-friendly interface for event management

---

## Setup Instructions

### Prerequisites

Before you begin, make sure you have the following installed:

- [.NET Core SDK](https://dotnet.microsoft.com/download) (version 7.0 or later)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express)
- A compatible IDE, such as [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

### Steps to Set Up the Project

1. **Clone the Repository:**
   ```bash
   git clone https://github.com/Moayadhamdan/Event-Registration-System.git
   cd Event-Registration-System

2. **Open the Solution:**
   * Navigate to the cloned repository folder and open the `.sln` file using Visual Studio 2022.

3. **Update the Database:**
   * In Visual Studio, open the Package Manager Console by navigating to `Tools > NuGet Package Manager > Package Manager Console`.
   * Run the following command to apply migrations and update the database:
     ```bash
     Update-Database
     ```
4. **Configure Mailjet:**
   * Sign up at Mailjet and create an account.
   * Navigate to Account Settings > API Keys and note down your API Key and Secret Key.
   * Add the Mailjet configuration to your appsettings.json:
```bash
   "Mailjet": {
    "ApiKey": "your_api_key",
    "ApiSecret": "your_api_secret"
} 
```

5. **Install Mailjet NuGet Package:**
   * Run the following command in the Package Manager Console:
      ```bash
        Install-Package Mailjet.Client
     ```

6. **Run the Project:**
   * Press F5 or click Start in Visual Studio to run the project.


## Testing Registration and Email Functionality

### Register for an Event:
1. Navigate to the Events page and select an event.
2. Click on the Register button and fill out the registration form.
3. Submit the form.

### Check Email Notifications:
- Ensure that you receive a confirmation email regarding your registration.
- Check your spam folder if the email does not appear in your inbox.

### Review the Database:
- Use SQL Server Management Studio (SSMS) to connect to your database and verify that the registration details are correctly stored.

## Technologies Used
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Mailjet (for email notifications)
- Bootstrap 5 (for styling)
- jQuery (for frontend functionality)
