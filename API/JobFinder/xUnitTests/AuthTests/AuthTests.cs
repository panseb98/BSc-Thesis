using Authorization.Models;
using Authorization.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace xUnitTests.AuthTests
{
    public class AuthTests
    {
        /* private readonly IAuthService _service;
         public AuthTests(IAuthService service)
         {
             _service = service;
         }

         [Fact]
         public async void Add_New_User()
         {
             List<EducationUser> generateEducationList()
             {
                 List<EducationUser> list = new List<EducationUser>();
                 list.Add(new EducationUser() { FieldOfStudy = "field of study 1", IsActive = true, IsNow = true, StartDate = DateTime.Now, StudyLevel = "study level 1", UniversityName = "university name 1" });
                 list.Add(new EducationUser() { FieldOfStudy = "field of study 2", IsActive = true, IsNow = true, StartDate = DateTime.Now, StudyLevel = "study level 2", UniversityName = "university name 2" });
                 return list;
             }

             List<ExperienceUser> generateExperienceList()
             {
                 List<ExperienceUser> experienceUsers = new List<ExperienceUser>();
                 experienceUsers.Add(new ExperienceUser() { CompanyName = "company name1", Description = "description", IsActive = true, IsNow = true, StartDate = DateTime.Now, PositionName = "position name", });
                 return experienceUsers;
             }

             List<SkillUser> generateSkillList()
             {
                 List<SkillUser> list = new List<SkillUser>();
                 list.Add(new SkillUser() {SkillId = 1 });
                 return list;
             }

             PersonalDataUser generatePersonalData()
             {
                 return new PersonalDataUser() { FirstName = "Jan", LastName = "Bednarek" };
             }

             var mainUser = new RegisterUser()
             {
                 Educations = generateEducationList(),
                 Email = "dupa@dupa.pl",
                 Skills = generateSkillList(),
                 Experiences = generateExperienceList(),
                 Password = "qw12qwas"
             };

             var result = await _service.RegisterNewUser(mainUser);

             Assert.True(result.IsLeft);
         }
     }*/
    }
}
