using TaskManager.Application.DTOs.Requests;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastructure.Persistence;
using TaskManager.Infrastructure.Services;

// === COMPOSITION ROOT: wiring everything together ===
// this is the ONLY place that knows about concrete classes
// everywhere else depends on interfaces (DIP)

var store = new InMemoryStore();

IProjectService projectService = new ProjectService(store);
ITaskService taskService = new TaskService(store);

// ============================================================
// 1. Create Projects
// ============================================================
Console.WriteLine("=== Create Projects ===\n");

var webApp = projectService.Create(new CreateProjectRequest("Web App", "Build the company website"));
var mobileApp = projectService.Create(new CreateProjectRequest("Mobile App", "Build the mobile application"));

Console.WriteLine($"  Created: {webApp.Name} (Id: {webApp.Id})");
Console.WriteLine($"  Created: {mobileApp.Name} (Id: {mobileApp.Id})");

Console.WriteLine($"\n  All projects: {projectService.GetAll().Count}");

// ============================================================
// 2. Create Tasks inside Projects
// ============================================================
Console.WriteLine("\n=== Create Tasks ===\n");

var task1 = taskService.Create(new CreateTaskRequest("Setup database", "Install SQL Server", webApp.Id));
var task2 = taskService.Create(new CreateTaskRequest("Design homepage", "Create UI mockup", webApp.Id));
var task3 = taskService.Create(new CreateTaskRequest("Setup Flutter", "Initialize Flutter project", mobileApp.Id));

Console.WriteLine($"  Created: {task1.Title} → Web App");
Console.WriteLine($"  Created: {task2.Title} → Web App");
Console.WriteLine($"  Created: {task3.Title} → Mobile App");

// check project now shows task count
var webAppUpdated = projectService.GetById(webApp.Id);
Console.WriteLine($"\n  Web App tasks: {webAppUpdated!.TotalTasks}");
Console.WriteLine($"  Mobile App tasks: {projectService.GetById(mobileApp.Id)!.TotalTasks}");