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