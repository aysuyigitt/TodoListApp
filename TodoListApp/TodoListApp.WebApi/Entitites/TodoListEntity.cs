using System.Text.Json.Serialization;

namespace TodoListApp.WebApi.Entitites
{
    public class TodoListEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [JsonIgnore]
        public List<TaskTodo> TaskTodos { get; set; } = new List<TaskTodo>();
    }
}
