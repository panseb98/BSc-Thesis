using AutoMapper;
using Database.Models.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobSeeker.Models
{
    public class EducationUser
    {
        public int? Id { get; set; }
        public string UniversityName { get; set; }
        public string FieldOfStudy { get; set; }
        public string StudyLevel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsNow { get; set; }
    }

    public class EducationUserMappingProfile : Profile
    {
        public EducationUserMappingProfile()
        {
            CreateMap<EducationUser, UserEducation>();
            CreateMap<UserEducation, EducationUser>();
        }
    }
}
