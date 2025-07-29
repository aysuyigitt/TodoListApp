namespace TodoListApp.WebApi.Entitites
{
    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public int TaskTodoId { get; set; }

        public TaskTodo TaskTodo { get; set; }
    }
}
