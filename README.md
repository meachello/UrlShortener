# URL Shortener

## Project Description
This project is a URL shortening service that allows users to create short links, manage them, and view information about created records.

## Technology Stack
- **Backend:** ASP.NET MVC (Framework/Core)
- **Frontend:** React
- **ORM:** Entity Framework (Code-First)
- **Database:** MySQL 

## Features
### 1. Authentication and User Roles
- Ability to log in using **Login** (username/password).
- Two user roles:
  - **Administrator** – can manage all records (view, add, delete, and edit information).
  - **Regular user** – can add, view, and delete only their own records.
- Anonymous users can only view the list of shortened links.

### 2. URL Management
- **Short URLs Table:**
  - Displays all records with original and shortened URLs.
  - Allows adding a new URL (only for authorized users).
  - Enables deletion of URLs (authors can delete their own, administrators can delete any).
- **Short URL Info Page:**
  - Available only to authorized users.
  - Displays information about the created URL (author, creation date, and additional details).

### 3. About Page
- Contains a description of the URL shortening algorithm.
- Accessible to all users.
- Editable only by administrators.

## Future Improvements
- **Implement Unit Tests** (not completed yet).
- Improve UI/UX design.
- Optimize database performance.
