# TaskWave

The "TaskWave" project is focused on creating a system that facilitates the management of users, groups, projects, and tasks within an organization. It allows system administrators to manage users and groups, assign roles and permissions, and oversee project-related activities. The system supports task management, including the creation, assignment, and tracking of tasks, as well as establishing task relationships to ensure workflow dependencies are maintained. Additionally, the project includes features for time logging and monitoring task progress, enabling users to track how long tasks take and identify areas for improvement. The system also emphasizes the importance of handling complex task relationships and soft deletion to prevent data loss.

# Persistance

There is MariaDB used as a database storage for this solution.

## Migrations

```shell
dotnet ef migrations add UserBase -s src/TaskWave.Api -p src/TaskWave.Infrastructure -o Persistence/Migrations
```
