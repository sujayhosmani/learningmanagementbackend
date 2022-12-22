using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using LearningManagement.Domain.DTOs;
using LearningManagement.Domain.Entities;

namespace LearningManagement.Common.Mappings
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<User, UserDto>().ForMember(dest => dest.Password, opt => opt.Ignore());

                config.CreateMap<User, UserDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
