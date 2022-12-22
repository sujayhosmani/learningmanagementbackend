using LearningManagement.Domain.Entities;
using LearningManagement.Infrastructure.Configurations;
using LearningManagement.Infrastructure.Interfaces;
using LearningManagement.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Infrastructure.DbContexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IUserService _userService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserService userService) : base(options)
        {
            _userService = userService; 
        }

        public DbSet<User> Users { get; set; }

        public async Task<int> SaveDbAsync(CancellationToken cancellationToken, string name = "")
        {
            foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                DateTime date = DateTime.UtcNow;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _userService.GetUserName(name);
                        entry.Entity.CreatedTimestamp = date.Date + new TimeSpan(date.Hour, date.Minute, date.Second);
                        entry.Entity.UpdatedBy = _userService.GetUserName(name);
                        entry.Entity.UpdatedTimestamp = date.Date + new TimeSpan(date.Hour, date.Minute, date.Second);
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _userService.GetUserName(name);
                        entry.Entity.UpdatedTimestamp = date.Date + new TimeSpan(date.Hour, date.Minute, date.Second);
                        break;
                }
            }
            return await this.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
        }

        
    }
}
