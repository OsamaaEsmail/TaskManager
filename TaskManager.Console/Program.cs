using System.Diagnostics.Metrics;
using TaskManager.Application.DTOs.Requests;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
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


// ============================================================
// 3. Complete and Reopen Tasks
// ============================================================
Console.WriteLine("\n=== Complete & Reopen Tasks ===\n");

// complete task1
taskService.Complete(task1.Id);
var completedTask = taskService.GetById(task1.Id);
Console.WriteLine($"  {completedTask!.Title}: IsCompleted = {completedTask.IsCompleted}");
Console.WriteLine($"  CompletedAt: {completedTask.CompletedAt}");

// check project progress
var webAppProgress = projectService.GetById(webApp.Id);
Console.WriteLine($"\n  Web App: {webAppProgress!.CompletedTasks}/{webAppProgress.TotalTasks} completed");

// reopen task1
taskService.Reopen(task1.Id);
var reopenedTask = taskService.GetById(task1.Id);
Console.WriteLine($"\n  After Reopen: {reopenedTask!.Title}: IsCompleted = {reopenedTask.IsCompleted}");

// try to complete already completed — should throw
taskService.Complete(task1.Id);
try
{
    taskService.Complete(task1.Id);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"\n  Error: {ex.Message}");
}


// ============================================================
// 4. Delete Tasks and Projects
// ============================================================
Console.WriteLine("\n=== Delete ===\n");

Console.WriteLine($"  Before delete: Web App tasks = {projectService.GetById(webApp.Id)!.TotalTasks}");

taskService.Delete(task2.Id);
Console.WriteLine($"  After deleting '{task2.Title}': Web App tasks = {projectService.GetById(webApp.Id)!.TotalTasks}");

projectService.Delete(mobileApp.Id);
Console.WriteLine($"\n  After deleting Mobile App: total projects = {projectService.GetAll().Count}");

// try to delete again — should throw
try
{
    projectService.Delete(mobileApp.Id);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"  Error: {ex.Message}");
}

// ============================================================
// 5. DIP Proof: Console only knows interfaces
// ============================================================
Console.WriteLine("\n=== DIP Proof ===\n");

Console.WriteLine($"  projectService type: {projectService.GetType().Name}");
Console.WriteLine($"  taskService type: {taskService.GetType().Name}");
Console.WriteLine($"  But Console only sees: IProjectService, ITaskService");
Console.WriteLine($"  If we swap Infrastructure with a real DB — Console code stays the same!");

Console.WriteLine("\n=== Done! ===");
