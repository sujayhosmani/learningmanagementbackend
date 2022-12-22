using Course.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Infrastructure.Interfaces
{
    public interface ICourseRepository
    {
        Task<Courses> AddCourse(Courses course);
        Task<List<Courses>> CourserByTechnology(string tech);
        Task<List<Courses>> CourserByTechnologyAndDuration(string tech, int from, int to);
        Task<List<Courses>> GetAllCourses();
        Task<bool> DeleteCourse(string id);
        Task<Duration> CourseDuration();
        Task<List<string>> CourseTechnologies();
    }
}
