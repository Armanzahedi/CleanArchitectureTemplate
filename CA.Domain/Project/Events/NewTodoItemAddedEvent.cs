using CA.Domain.Common;
using CA.Domain.Project.Entities;

namespace CA.Domain.Project.Events;

public class NewTodoItemAddedEvent: DomainEventBase
{
    private TodoItem NewItem { get; }
    private Project Project { get; }

    public NewTodoItemAddedEvent(Project project,
        TodoItem newItem)
    {
        Project = project;
        NewItem = newItem;
    }
}