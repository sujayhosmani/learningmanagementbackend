using Course.Domain.Entity;
using LearningManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Infrastructure.Interfaces
{
    public interface ICourseDbContext
    {
        IMongoCollection<Courses> Courses { get; set; }
    }
}
