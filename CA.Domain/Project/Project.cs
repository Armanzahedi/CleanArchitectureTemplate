using Ardalis.GuardClauses;
using CA.Domain.Common;
using CA.Domain.Common.Attributes;
using CA.Domain.Common.Interfaces;
using CA.Domain.Project.Entities;
using CA.Domain.Project.Enums;
using CA.Domain.Project.Events;

namespace CA.Domain.Project;

[Auditable]
public class Project : EntityBase<Guid>, IAggregateRoot, ISoftDeletableEntity
{
    private Project()  {  }
    private string Title { get;}
    public string? Description { get; set; }
    
    private readonly List<TodoItem> _items = new();
    public IEnumerable<TodoItem> Items => _items.AsReadOnly();

    public Project(string title,string description)
    {
        Guard.Against.NullOrEmpty(title, nameof(Title));
        Guard.Against.NullOrEmpty(description, nameof(Description));
  
        this.Title = title;
        this.Description = description;
    }

    
    public void AddTodo(string title,string? description)
    {
        var item = new TodoItem(title, description);
        _items.Add(item);
        this.RegisterDomainEvent(new NewTodoItemAddedEvent(this,item));
    }

    public ProjectStatus Status => _items.All(i => i.Done) ? ProjectStatus.Done : ProjectStatus.Created;

    public bool IsDeleted { get; set; }
}