using System.Text.Json.Serialization;

namespace TodoListApp.WebApi.Entitites
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public List<TaskTodo> TaskTodos { get; set; } = new();
    }
}
