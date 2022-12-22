using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LearningManagement.Domain.Entities
{
    public class User : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        

    }
}
