using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Domain.Constants
{
    public class Constants
    {
        public const string GetAll = "getall";
        public const string GetByTech = "info/{technology}";
        public const string GetByDuration = "get/{from}/{to}/{technology}";
        public const string DeleteById = "delete/{id}";
        public const string Add = "add";
        public const string Tech = "technologies";
        public const string Duration = "durations";
    }
}
