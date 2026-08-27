# Workbench

Team work management with kanban boards, milestones, and issue tracking.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)
![Blazor](https://img.shields.io/badge/Blazor-Server-68217A?logo=blazor)
![EF Core](https://img.shields.io/badge/EF_Core-10.0-512BD4?logo=entityframework)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-17-4169E1?logo=postgresql)
![License](https://img.shields.io/badge/license-MIT-green)

## Features

- **Projects** — create and manage workspaces with team members
- **Kanban board** — drag-and-drop cards across columns with WIP limits
- **Milestones** — group issues by deadline with progress tracking
- **Issues** — track work with status, assignment, votes, and comments
- **Cross-references** — mention `#123` in comments to link issues
- **Attachments** — file uploads for issues and comments
- **Tags** — label and filter issues
- **Invite codes** — join without an admin

## Getting started

```bash
# clone
git clone https://github.com/Bahaaio/Workbench.git
cd Workbench/src/Workbench

docker compose up -d
dotnet run
```

## Tech stack

- ASP.NET Core 10 — Blazor Server UI
- Entity Framework Core + Npgsql (PostgreSQL)
- ASP.NET Core Identity (cookie auth, no roles)
- MudBlazor (UI components)
- xUnit + Moq (tests)
