# TaskManager — Clean Architecture + Onion Architecture Demo

A minimal .NET 9 project demonstrating Clean Architecture & Onion Architecture principles with OOP and SOLID through a Task Manager domain.

## Project Structure
```
TaskManager/
├── TaskManager.Domain/              (Core - innermost layer)
│   └── Entities/
│       ├── TaskItem.cs
│       └── Project.cs
│
├── TaskManager.Application/         (Use Cases - contracts & DTOs)
│   ├── DTOs/
│   │   ├── Requests/
│   │   │   ├── CreateProjectRequest.cs
│   │   │   └── CreateTaskRequest.cs
│   │   └── Responses/
│   │       ├── ProjectResponse.cs
│   │       └── TaskItemResponse.cs
│   └── Interfaces/
│       ├── IProjectService.cs
│       └── ITaskService.cs
│
├── TaskManager.Infrastructure/      (Implementation - outermost layer)
│   ├── Persistence/
│   │   └── InMemoryStore.cs
│   └── Services/
│       ├── ProjectService.cs
│       └── TaskService.cs
│
└── TaskManager.Console/             (Entry Point - Composition Root)
    └── Program.cs
```

## Architecture Layers

| Layer | Knows About | Contains | Rule |
|-------|-------------|----------|------|
| **Domain** | Nothing | Entities | The core — no dependencies on anything |
| **Application** | Domain | DTOs, Interfaces | Defines contracts — what the system CAN do |
| **Infrastructure** | Application + Domain | Services, Persistence | Implements contracts — how the system DOES it |
| **Console** | Application + Infrastructure | Program.cs | Composition Root — wires everything together |

## Dependency Flow (Onion Architecture)
```
Console → Application + Infrastructure
Infrastructure → Application + Domain
Application → Domain
Domain → Nothing ❌
```

Dependencies always point INWARD. The Domain never knows about the outside world.

## OOP & SOLID Principles Applied

| Principle | Where | How |
|-----------|-------|-----|
| **SRP** | Every class | ProjectService handles projects only. TaskService handles tasks only. DTOs carry data only. |
| **OCP** | Services + InMemoryStore | Swap InMemoryStore with a real database — Services stay the same. |
| **LSP** | IProjectService, ITaskService | Any implementation can replace another through the interface. |
| **ISP** | IProjectService, ITaskService | Separate interfaces — not one giant interface for everything. |
| **DIP** | Services + Console | Services implement interfaces. Console depends on interfaces, not concrete classes. |
| **Encapsulation** | DTOs as records | Immutable data carriers — no one can modify them after creation. |
| **Abstraction** | Interfaces | Application defines WHAT — Infrastructure decides HOW. |
| **Association** | Project → TaskItem | One-to-many relationship between Project and Tasks. |
