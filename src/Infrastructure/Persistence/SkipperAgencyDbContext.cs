using System.Data;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Common;
using SkipperAgency.Domain.Entities;

namespace SkipperAgency.Infrastructure.Persistence
{
    public class SkipperAgencyDbContext : IdentityDbContext<AppUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private IDbContextTransaction _currentTransaction;

        public SkipperAgencyDbContext(
            DbContextOptions options,
            ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
        }

        public DbSet<TodoList> TodoLists { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<Skipper> Skipper { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Charter> Charter { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Boat> Boats { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingHistory> BookingHistories { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Licence> Licences { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RegionAvailability> RegionAvailabilities { get; set; }
        public DbSet<TrustedCharterSkipper> TrustedSkippers { get; set; }
        public DbSet<UnTrustedCharterSkipper> UnTrustedSkippers { get; set; }
        public DbSet<SkipperLanguage> SkipperLanguages { get; set; }
        public DbSet<SkipperSkill> SkipperSkills { get; set; }
        public DbSet<PreRegisterSkipper> SkipperPreRegistration { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction = await base.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
