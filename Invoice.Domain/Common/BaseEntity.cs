namespace Invoice.Domain.Common;
public abstract class BaseEntity : IEntity<Guid>, ICreatedEntity, IModifiedEntity, ISoftDeleted
{
    [Required]
    [Display(Name = "شناسه")]
    public Guid Id { get; set; } = Guid.NewGuid();
}


