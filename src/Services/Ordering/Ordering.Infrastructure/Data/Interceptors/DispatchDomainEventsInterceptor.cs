using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace Ordering.Infrastructure.Data.Interceptors;

public class DispatchDomainEventsInterceptor(IMediator meditar) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    public async Task DispatchDomainEvents(DbContext? context)
    {
        if (context == null) return;

        var aggregates = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(a => a.Entity.DomainEvents.Any())
            .Select(a => a.Entity);

        var domainEvents = aggregates
            .SelectMany(a => a.DomainEvents) // ✅ Flatten the list of lists
            .ToList();

        foreach (var aggregate in aggregates)
            aggregate.ClearDomainEvent();

        foreach (var domainEvent in domainEvents)
            await meditar.Publish(domainEvent); // ✅ Now domainEvent is of type IDomainEvent : INotification
    }

}
