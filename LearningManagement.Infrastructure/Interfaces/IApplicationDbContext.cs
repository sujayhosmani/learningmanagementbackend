using LearningManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Infrastructure.Interfaces
{
    public interface IApplicationDbContext
    {
        Task<int> SaveDbAsync(CancellationToken cancellationToken, string name);

        DbSet<User> Users { get; set; }
    }
}
