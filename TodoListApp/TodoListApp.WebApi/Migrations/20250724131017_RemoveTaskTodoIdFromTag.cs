using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoListApp.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTaskTodoIdFromTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_TaskTodos_TaskTodoId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TaskTodoId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TaskTodoId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "TagTaskTodo",
                columns: table => new
                {
                    TagsId = table.Column<int>(type: "int", nullable: false),
                    TaskTodosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTaskTodo", x => new { x.TagsId, x.TaskTodosId });
                    table.ForeignKey(
                        name: "FK_TagTaskTodo_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagTaskTodo_TaskTodos_TaskTodosId",
                        column: x => x.TaskTodosId,
                        principalTable: "TaskTodos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagTaskTodo_TaskTodosId",
                table: "TagTaskTodo",
                column: "TaskTodosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagTaskTodo");

            migrationBuilder.AddColumn<int>(
                name: "TaskTodoId",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TaskTodoId",
                table: "Tags",
                column: "TaskTodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_TaskTodos_TaskTodoId",
                table: "Tags",
                column: "TaskTodoId",
                principalTable: "TaskTodos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
