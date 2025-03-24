# Advent Echo

Welcome to **Advent Echo**! This application is a digital platform designed to serve as a modern bulletin board for Adventist churches. It allows pastors, elders, deacons, and department leaders to share notices, schedules, and updates, ensuring the congregation stays connected and informed.

## Purpose

Advent Echo aims to bring the traditional church bulletin board into the digital age, creating a centralized space where the Adventist community can:
- Share schedules for singers, preachers, and other roles
- Post notices from church leadership
- Highlight departmental updates (e.g., music, youth, Sabbath School)
- Strengthen communication with a simple, accessible tool

## Features (Planned or Current) 

- TODO: In progress
- **Notices**: Create, edit, and delete notices with timestamps and details.
- **Schedules**: Manage rosters for church activities and roles.
- **Departments**: Organize content by church ministries (e.g., music, youth).
- **User Roles**: Permissions for pastors, elders, deacons, and members.
- **Responsive Design**: Works seamlessly on desktops, tablets, and phones.

*(Note: Update this section based on your progress or plans!)*
> [Excalidraw](https://excalidraw.com/#json=y6XlZ-9mA1CWu_sPZO_AK,GEbbj0MhhPdTm6R4yaCIew) 

## Getting Started

- TODO: In progress

### Prerequisites
- TODO: In progress

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/geeksevenlabs/advent-echo.git
   ``` 

## Running Entity Framework Migrations

You can use the provided scripts to create and apply database migrations for different contexts.

### **For Linux/macOS (Bash)**
1. Open a terminal.
2. Navigate to the project root folder.
3. Run the script:  
   ```sh
   chmod +x create-migration.sh  # Only needed the first time
   ./create-migration.sh
   ```
4. Follow the on-screen prompts:
   - Choose the database context (`AdventEchoIdentityDbContext` or `AdventEchoDbContext`).
   - Enter the migration name.
   - Confirm if you want to update the database.

### **For Windows (PowerShell)**
1. Open **PowerShell**.
2. Navigate to the project root folder.
3. If it's your first time running scripts, allow execution with:  
   ```powershell
   Set-ExecutionPolicy RemoteSigned -Scope CurrentUser
   ```
   Press `Y` and `Enter`.
4. Run the script:  
   ```powershell
   .\create-migration.ps1
   ```
5. Follow the on-screen prompts:
   - Choose the database context (`AdventEchoIdentityDbContext` or `AdventEchoDbContext`).
   - Enter the migration name.
   - Confirm if you want to update the database.

These scripts simplify the migration process by guiding you through context selection, migration creation, and database updates. 🚀
