using AutoMapper;
using Course.Domain.Entity;
using Course.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Course.Infrastructure.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ICourseDbContext _context;

        public CourseRepository(ICourseDbContext courseDbContext)
        {
            _context = courseDbContext;
        }

        public async Task<Courses> AddCourse(Courses course)
        {
           await _context.Courses.InsertOneAsync(course);
            return course;
        }

        public async Task<Duration> CourseDuration()
        {
            List<Courses> courses = await _context.Courses.Find(x => true).SortByDescending(d => d.Duration).Limit(1).ToListAsync();
            Duration duration = new Duration();
            if (courses.Any())
            {
                duration.From = 0;
                duration.To = courses.First().Duration;
            }
            return duration;
        }

        public async Task<List<Courses>> CourserByTechnology(string tech)
        {
            FilterDefinition<Courses> filter = Builders<Courses>.Filter.Eq(p => p.Technology, tech);

            return await _context
                            .Courses
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<List<Courses>> CourserByTechnologyAndDuration(string tech, int from, int to)
        {
            var builder = Builders<Courses>.Filter;
            FilterDefinition<Courses> filter;
            if(tech.IsNullOrEmpty())
            {
                filter = builder.Where(t => t.Duration >= from) & builder.Where(t => t.Duration <= to);
            }
            else
            {
                filter = builder.Eq(t => t.Technology, tech) & builder.Where(t => t.Duration >= from) & builder.Where(t => t.Duration <= to);
            }

            List<Courses> res = await _context
                            .Courses
                            .Find(filter)
                            .ToListAsync();
            return res;
        }

        public async Task<List<string>> CourseTechnologies()
        {
            return await _context.Courses.Distinct<string>("Technology", FilterDefinition<Courses>.Empty).ToListAsync();

        }

        public async Task<bool> DeleteCourse(string id)
        {
            FilterDefinition<Courses> filter = Builders<Courses>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Courses
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<List<Courses>> GetAllCourses()
        {
            return await _context.Courses.Find(p => true).ToListAsync();
        }
    }
}
