namespace Invoice.Infrastructure.Interceptors;

public class SaveChangesInterceptor : Microsoft.EntityFrameworkCore.Diagnostics.SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _contextAccessor;

    public SaveChangesInterceptor(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null)
            return base.SavingChanges(eventData, result);

        var currentUserId = _contextAccessor.HttpContext.GetUserId();
        var currentUserIP = _contextAccessor.HttpContext.GetUserIP();

        foreach (var entry in eventData.Context.ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
        {

            entry.Property("CreatedDateTime").CurrentValue = DateTime.UtcNow;
            entry.Property("CreatedByUserId").CurrentValue = currentUserId;
            entry.Property("CreatedByIP").CurrentValue = currentUserIP;
        }

        foreach (var entry in eventData.Context.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified))
        {

            entry.Property("ModifiedDateTime").CurrentValue = DateTime.UtcNow;
            entry.Property("ModifiedByUserId").CurrentValue = currentUserId;
            entry.Property("ModifiedByIP").CurrentValue = currentUserIP;
        }

        foreach (var entry in eventData.Context.ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted))
        {

            entry.State = EntityState.Modified;
            entry.Property("DeletedDateTime").CurrentValue = DateTime.UtcNow;
            entry.Property("DeletedByUserId").CurrentValue = currentUserId;
            entry.Property("DeletedByIP").CurrentValue = currentUserIP;
            entry.Property("IsDeleted").CurrentValue = true;
        }

        return base.SavingChanges(eventData, result);
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        Guid? currentUserId = null;
        if (_contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated == true)
        {
            currentUserId = _contextAccessor.HttpContext.GetUserId();
        }

        var currentUserIP = _contextAccessor.HttpContext.GetUserIP();

        foreach (var entry in eventData.Context.ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
        {

            SetValueIfExists(entry, "CreatedDateTime", DateTime.UtcNow);
            SetValueIfExists(entry, "CreatedByUserId", currentUserId);
            SetValueIfExists(entry, "CreatedByIP", currentUserIP);
        }

        foreach (var entry in eventData.Context.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified))
        {

            SetValueIfExists(entry, "ModifiedDateTime", DateTime.UtcNow);
            SetValueIfExists(entry, "ModifiedByUserId", currentUserId);
            SetValueIfExists(entry, "ModifiedByIP", currentUserIP);
        }

        foreach (var entry in eventData.Context.ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted))
        {

            entry.State = EntityState.Modified;

            SetValueIfExists(entry, "DeletedDateTime", DateTime.UtcNow);
            SetValueIfExists(entry, "DeletedByUserId", currentUserId);
            SetValueIfExists(entry, "DeletedByIP", currentUserIP);
            SetValueIfExists(entry, "IsDeleted", true);
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    /// <summary>
    /// فقط در صورتی که Property در Entity وجود داشت، مقدار می‌دهد.
    /// </summary>
    private void SetValueIfExists(EntityEntry entry, string propertyName, object? value)
    {
        if (entry.Metadata.FindProperty(propertyName) != null)
        {
            entry.Property(propertyName).CurrentValue = value;
        }
    }
}

