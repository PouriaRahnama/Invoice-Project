namespace Invoice.Domain.Common;

public interface IEntity<TKey>
{
    public TKey Id { get; set; }
    public bool IsActive { get; set; }
}


public interface ICreatedEntity { }
public interface IModifiedEntity { }
public interface ISoftDeleted { }


