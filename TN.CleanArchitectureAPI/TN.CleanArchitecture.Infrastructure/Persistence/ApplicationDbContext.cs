using TN.Prototype.CleanArchitecture.Application.Common.Interfaces;
using TN.Prototype.CleanArchitecture.Domain.Common;
using TN.Prototype.CleanArchitecture.Domain.Entities;
using TN.Prototype.CleanArchitecture.Infrastructure.Persistence.Constants;
using TN.Prototype.CleanArchitecture.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TN.Prototype.CleanArchitecture.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTimeService,
            ICurrentUserService currentUserService, IDomainEventService domainEventService) : base(options)
        {
            _dateTimeService = dateTimeService;
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>().ToTable(nameof(Employee));
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Creator = _currentUserService.UserId;
                        entry.Entity.CreateDate = _dateTimeService.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.Modifier = _currentUserService.UserId;
                        entry.Entity.ModifyDate = _dateTimeService.Now;
                        break;
                }
            }

            var events = ChangeTracker.Entries<IHasDomainEvent>()
                                      .Select(x => x.Entity.DomainEvents)
                                      .SelectMany(x => x)
                                      .Where(domainEvent => !domainEvent.IsPublished)
                                      .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);
            await DispatchEvents(events);
            return result;
        }

        private async Task DispatchEvents(DomainEvent[] events)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await _domainEventService.Publish(@event);
            }
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
