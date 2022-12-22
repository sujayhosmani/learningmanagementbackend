﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Domain.DTOs
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string? Token { get; set; }
        public string Role { get; set; }
    }
}
