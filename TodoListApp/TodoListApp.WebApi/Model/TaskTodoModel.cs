namespace TodoListApp.WebApi.Model
{
    public class TaskTodoModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DueDate { get; set; }

        public string Status { get; set; }

        public string AssignedTo { get; set; }

        public string TodoName { get; set; }

        public int TodoListEntityId { get; set; }

    }
}
