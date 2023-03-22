using FluentValidation;

namespace CA.Application.Projects.Commands.AddTodo;

public class AddTodoCommandValidator : AbstractValidator<AddTodoCommand>
{
    public AddTodoCommandValidator()
    {
        RuleFor(x => x.todoTile).NotEmpty();
    }
}