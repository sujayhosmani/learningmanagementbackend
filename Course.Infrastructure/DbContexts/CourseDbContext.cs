using Course.Domain.Entity;
using Course.Infrastructure.Interfaces;
using LearningManagement.Domain.Entities;
using LearningManagement.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Infrastructure.DbContexts
{
    public class CourseDbContext : ICourseDbContext
    {

        public CourseDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("DefaultConnection"));
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

            Courses = database.GetCollection<Courses>(configuration["DatabaseSettings:CollectionName"]);
        }

        public IMongoCollection<Courses> Courses { get; set; }

    }
}
