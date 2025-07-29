using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TodoListApp.WebApi.Entitites;
using TodoListApp.WebApp.Models;

public class TaskTodoController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public TaskTodoController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index(int id)
    {
        var tasks = await GetTaskTodoByTodoListByIdAsync(id);
        return View(tasks);
    }

    public async Task<List<TaskTodoWebApiModel>> GetTaskTodoByTodoListByIdAsync(int todoListId)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"http://localhost:22096/api/TaskTodos/GetTaskTodoByTodoListById?id={todoListId}");

        if (response.IsSuccessStatusCode)
        {
            var jsonData = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TaskTodoWebApiModel>>(jsonData);
            return result!;
        }

        return new List<TaskTodoWebApiModel>();
    }

    [HttpGet]
    public IActionResult CreateTaskTodo()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> CreateTaskTodo(TaskTodoWebApiModel taskTodoWebApiModel)
    {
        var client = _httpClientFactory.CreateClient();
        var jsonData = JsonConvert.SerializeObject(taskTodoWebApiModel);
        var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("http://localhost:22096/api/TaskTodos", stringContent);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", new { id = taskTodoWebApiModel.Id });
        }

        return View();
    }

    public async Task<IActionResult> DeleteTaskTodo(int id, int todoListId)
    {
        var client = _httpClientFactory.CreateClient();
        var responseMessage = await client.DeleteAsync($"http://localhost:22096/api/TaskTodos/{id}");
        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("Index", new { id = todoListId });
        }

        return View();
    }
}