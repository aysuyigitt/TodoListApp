﻿namespace TodoListApp.WebApp.Models
{
    public class TaskTodoWebApiModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime DueDate { get; set; }

        public string Status { get; set; }

    }
}
