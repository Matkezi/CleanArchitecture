﻿using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }
        DbSet<AppUser> AppUser { get; set; }
        DbSet<Skipper> Skipper { get; set; }
        DbSet<Skill> Skills { get; set; }
        DbSet<Charter> Charter { get; set; }
        DbSet<Availability> Availabilities { get; set; }
        DbSet<Boat> Boats { get; set; }
        DbSet<Booking> Bookings { get; set; }
        DbSet<BookingHistory> BookingHistories { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Licence> Licences { get; set; }
        DbSet<Region> Regions { get; set; }
        DbSet<RegionAvailability> RegionAvailabilities { get; set; }
        DbSet<TrustedCharterSkipper> TrustedSkippers { get; set; }
        DbSet<UnTrustedCharterSkipper> UnTrustedSkippers { get; set; }
        DbSet<SkipperSkill> SkipperSkills { get; set; }
        DbSet<PreRegisterSkipper> SkipperPreRegistration { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
