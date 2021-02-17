using Authorization.Models;
using AutoMapper;
using Database.Models.Authorization;
using JobSeeker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authorization.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<EducationUser, UserEducation>();
            CreateMap<UserEducation, EducationUser>();

            CreateMap<ExperienceUser, UserExperience>();
            CreateMap<UserExperience, ExperienceUser>();

            CreateMap<UserSkill, SkillUser>();
            CreateMap<SkillUser, UserSkill>();
        }
    }
}
