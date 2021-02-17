using AutoMapper;
using Database.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authorization.Models
{
    public class RoleModel
    {
        public string Name { get; set; }
    }

    public class RoleModelMappingProfile : Profile
    {
        public RoleModelMappingProfile()
        {
            CreateMap<RoleModel, Role>();
            CreateMap<Role, RoleModel>();
        }
    }
}
