using AutoMapper;
using Course.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Domain.Mappings
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<User, UserDto>().ForMember(dest => dest.Password, opt => opt.Ignore());

                
            });

            return mappingConfig;
        }
    }
}