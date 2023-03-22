using Ardalis.GuardClauses;
using CA.Domain.Common;
using CA.Domain.Common.Attributes;

namespace CA.Domain.Project.Entities;

[Auditable]
public class TodoItem : EntityBase
{
    private TodoItem(){}

    public TodoItem(string title, string? description)
    {
        Guard.Against.NullOrEmpty(title, nameof(Title));
        this.Title = title;
        this.Description = description??"";
    }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Done { get; set; }
}