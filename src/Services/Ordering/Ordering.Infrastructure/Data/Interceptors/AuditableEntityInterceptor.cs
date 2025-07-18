﻿using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors;

public class AuditableEntityInterceptor:SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntites(eventData.Context);
        return base.SavingChanges(eventData, result);
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntites(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntites(DbContext? context)
    {
        if (context == null) return;

        foreach(var entry in context.ChangeTracker.Entries<IEntity>())
        {
            if(entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = "Cp";
                entry.Entity.CreatedAt=DateTime.UtcNow;
            }
            if (entry.State == EntityState.Added || entry.State ==EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.CreatedBy = "Cp";
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }
        }

    }

}

public static class Extension
{
    public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
       entry.References.Any(r =>
       r.TargetEntry != null &&
       r.TargetEntry.Metadata.IsOwned() &&
       (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}
