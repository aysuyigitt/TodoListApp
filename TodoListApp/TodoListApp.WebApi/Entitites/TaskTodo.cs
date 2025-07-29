using System.Text.Json.Serialization;

namespace TodoListApp.WebApi.Entitites
{
    public class TaskTodo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DueDate { get; set; }

        public string Status { get; set; }

        public string AssignedTo { get; set; }

        public int TodoListEntityId { get; set; }

        [JsonIgnore]
        public TodoListEntity TodoListEntity { get; set; }

        public List<Tag> Tags { get; set; } = new();

        public bool IsActive => Status == "Başlanmadı" || Status == "Devam Ediyor";

        public bool IsCompleted => Status == "Tamamlandı";

    }
}
