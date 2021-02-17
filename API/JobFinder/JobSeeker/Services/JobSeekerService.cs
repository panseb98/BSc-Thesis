using AutoMapper;
using Database;
using Database.Models.Authorization;
using Database.Models.InitJobOffers;
using Database.Models.JobSeeker;
using JobSeeker.Models;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Services
{
    public class JobSeekerService : IJobSeekerService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public JobSeekerService(DataContext dataContext, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<Either<Exception, bool>> AddNewJobRequest(NewJob model)
        {
            try
            {
                _dataContext.JobRequests.Add(new JobRequest() { UserId = Convert.ToInt32(model.UserId), Date = DateTime.Now, Location = model.Location, JobName = model.KeyName });
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Either<Exception, bool>> AddNewKey(int recordId)
        {
            try
            {
                var reqModel = _dataContext.KeyRequests.FirstOrDefault(x => x.Id == recordId);
                _dataContext.JobSkills.Add(new Database.Models.JobSkill() { Key = reqModel.KeyName});
                await _dataContext.SaveChangesAsync();

                _dataContext.KeyRequests.Remove(reqModel);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return new Exception("Ups, coś poszło nie tak");
            }
        }

        public async Task<Either<Exception, bool>> AddNewKeyRequest(NewKey model)
        {
            try
            {
                var reqCount = _dataContext.KeyRequests.Count(x => x.KeyName.ToLower() == model.KeyName.ToLower());
                if (!reqCount.Equals(0))
                {
                    return new Exception("Taka umiejętność już czeka na dodanie!");
                }

                var skillCount = _dataContext.JobSkills.Count(x => x.Key.ToLower() == model.KeyName.ToLower());
                if (!skillCount.Equals(0))
                {
                    return new Exception("Taka umiejętność już istnieje!");
                }
                _dataContext.KeyRequests.Add(new KeyRequest() { UserId = Convert.ToInt32(model.UserId), Date = DateTime.Now, KeyName = model.KeyName });
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return new Exception("Ups, coś poszło nie tak");
            }
        }
        public async Task<Either<Exception, RequestList>> GetAllRequests()
        {
            try
            {
                var keyList = await _dataContext
                    .KeyRequests
                    .Select(x => new NewKey() {KeyName = x.KeyName, UserId = x.UserId.ToString(), Date = x.Date, Id = x.Id  })
                    .ToListAsync();

                var jobList = await _dataContext
                    .JobRequests
                    .Select(x => new NewJob() { Id = x.Id, Date = x.Date, KeyName = x.JobName, Location = x.Location, UserId = x.UserId.ToString() })
                    .ToListAsync();

                return new RequestList() { JobRequests = jobList, KeyRequests = keyList };
            }
            catch
            {
                return new Exception("Ups, coś poszło nie tak");
            }
        }

        public async Task<Either<Exception, List<string>>> GetAllSkills()
        {
            return await _dataContext.JobSkills.Select(x => x.Key).ToListAsync();
        }

        public async Task<Either<Exception, ExpendedUserData>> GetExpendedUserData(int userId)
        {

            /* var model = new ExpendedUserData()
             {
                 Educations = await _dataContext.UserEducations.Where(x => x.UserId == userId).Select(x => _mapper.Map<EducationUser>(x)).ToListAsync(),
                 Experiences = await _dataContext.UserExperiences.Where(x => x.UserId == userId).Select(x => _mapper.Map<ExperienceUser>(x)).ToListAsync(),
                 Skills = await _dataContext.UserSkills.Where(x => x.UserId == userId).Select(x => _mapper.Map<SkillUser>(x)).ToListAsync()
             };*/
             var model = new ExpendedUserData()
             {
                 Educations = await _dataContext.UserEducations.Where(x => x.UserId == userId).Select(x => new EducationUser() { EndDate = x.EndDate, FieldOfStudy = x.FieldOfStudy, Id = x.Id, IsNow = x.IsNow, StartDate = x.StartDate, StudyLevel = x.StudyLevel, UniversityName = x.UniversityName}).ToListAsync(),
                 Experiences = await _dataContext.UserExperiences.Where(x => x.UserId == userId).Select(x => new ExperienceUser() { StartDate = x.StartDate, IsNow = x.IsNow, Id = x.Id, CompanyName = x.CompanyName, Description = x.Description, EndDate = x.EndDate, PositionName = x.PositionName}).ToListAsync(),
                 Skills = await _dataContext.UserSkills.Where(x => x.UserId == userId).Select(x => new SkillUser() { SkillId = x.SkillId, SkillName =  _dataContext.JobSkills.First(b => b.Id == x.SkillId).Key}).ToListAsync()
             };

            return model;
        }

        public async  Task<Either<Exception, List<string>>> GetJobNames(string key)
        {
            if(key == "" || key is null)
            {
                return await _dataContext
                    .JobOffers
                    .Select(x => x.JobName)
                    .GroupBy(x => x)
                    .Select(group => group.Key)
                    .Take(50)
                    .ToListAsync();
            }
            return await _dataContext
                .JobOffers
                .Select(x => x.JobName)
                .GroupBy(x => x)
                .Select(group => group.Key.ToLower())
                .Where(x => x.Contains(key.ToLower()))
                .Take(50)
                .ToListAsync();
        }

        public async Task<Either<Exception, List<RecommendedJobModel>>> GetRecommendedJobs(int userId)
        {
            var finalJobs = new List<RecommendedJobModel>();
            var userSkills = await _dataContext.UserSkills.Where(x => x.UserId == userId)
                .Select(x => _dataContext.JobSkills
                .First(b => b.Id == x.SkillId)
                .Key.ToLower())
                .ToListAsync();
            RecommendedJobModel returnViewModel(JobOffer model )
            {
                return null;
            }

            if (userSkills.Count().Equals(0))
                return new Exception("Uzupełnij liste umiejętniści!");
            try
            {
                foreach (var item in _dataContext.JobOffers)
                {
                    var jobSkills = item
                        .Keys
                        .Split(';')
                        .ToList();

                    var jointedList = jobSkills
                        .Intersect(userSkills).ToList();
                        
                    if (!jointedList.Count().Equals(0))
                    {
                        finalJobs.Add(new RecommendedJobModel()
                        {
                            JobDesc = item.JobDesc,
                            JobId = item.Id,
                            JobShortDesc = item.JobDesc.Substring(0, 50) + "...",
                            JobRaiting =  Convert.ToInt32(((double)jointedList.Count() / (double)jobSkills.Count()) * 100),
                            JobTitle = item.JobName,
                            JobSkills = string.Join("; ", jobSkills),
                            JoinedSkills = string.Join(", ", jointedList)
                        }
                        );
                    }
                    
                    
                }

                finalJobs = finalJobs
                    .OrderByDescending(x => x.JobRaiting)
                    .Take(50)
                    .ToList();

                return finalJobs;
            }
            catch (Exception e)
            {
                return new Exception("Coś poszło nie tak, spróbuj ponownie później.");
            }
        }

        public async Task<Either<Exception, List<SkillUser>>> GetSkillsDropdownlist(string key)
        {
            if(key is null || key == "")
            {
                return await _dataContext
                    .JobSkills
                    .Select(x => new SkillUser() { SkillId = x.Id, SkillName = x.Key })
                    .Take(50)
                    .ToListAsync();
            }
            else
            {
                return await _dataContext
                    .JobSkills
                    .Where(x =>
                        x.Key.ToLower()
                        .Contains(key.ToLower()))
                    .Select(x => new SkillUser() { SkillId = x.Id, SkillName = x.Key })
                    .ToListAsync();
            }
            
        }

        public async Task<Either<Exception, bool>> RemoveNewKey(int recordId)
        {
            try
            {
                var reqModel = _dataContext.KeyRequests.FirstOrDefault(x => x.Id == recordId);

                _dataContext.KeyRequests.Remove(reqModel);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return new Exception("Ups, coś poszło nie tak");
            }
        }

        public async Task<Either<Exception, bool>> UpdateExpendedUserData(ExpendedUserData model)
        {
            try
            {
                _dataContext.UserEducations.RemoveRange(_dataContext.UserEducations.Where(x => x.UserId == model.UserId));
                _dataContext.UserExperiences.RemoveRange(_dataContext.UserExperiences.Where(x => x.UserId == model.UserId));
                _dataContext.UserSkills.RemoveRange(_dataContext.UserSkills.Where(x => x.UserId == model.UserId));
            }
            catch(Exception e)
            {
                var a = 1;
            }
            

            var newEducation = model.Educations.Select(x => new UserEducation{
                IsNow = x.IsNow,
                EndDate = x.EndDate,
                FieldOfStudy = x.FieldOfStudy,
                StartDate = x.StartDate,
                IsActive = true,
                StudyLevel = x.StudyLevel,
                UniversityName = x.UniversityName,
                UserId = model.UserId
            });
            var newSkills = model.Skills.Select(x => new UserSkill() { 
                UserId = model.UserId,
                SkillId = x.SkillId
            });
            var newExperience = model.Experiences.Select(x => new UserExperience() {
                StartDate = x.StartDate,
                CompanyName = x.CompanyName,
                Description = x.Description,
                EndDate = x.EndDate,
                IsActive = true,
                IsNow = x.IsNow,
                PositionName = x.PositionName,
                UserId = model.UserId
            });


            _dataContext.UserSkills.AddRange(newSkills);
            _dataContext.UserEducations.AddRange(newEducation);
            _dataContext.UserExperiences.AddRange(newExperience);

            await _dataContext.SaveChangesAsync();

            return true;
        }

        public async Task<Either<Exception, bool>> RemoveNewJob(int recordId)
        {
            try
            {
                var reqModel = _dataContext.JobRequests.FirstOrDefault(x => x.Id == recordId);

                _dataContext.JobRequests.Remove(reqModel);
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return new Exception("Ups, coś poszło nie tak");
            }
        }

        public async Task<Either<Exception, List<ExperienceUser>>> GetJobsFromCV(int userId)
        {
            try
            {
                var dbJobs = _dataContext
                    .UserExperiences
                    .Where(x => x.UserId == userId)
                    .Select(x => new ExperienceUser() { CompanyName = x.CompanyName, Description = x.Description, EndDate = x.EndDate, Id = x.Id, IsNow = x.IsNow, PositionName = x.PositionName, StartDate = x.StartDate });

                return await dbJobs.ToListAsync();
                
            }
            catch
            {
                return new Exception("Ups, coś poszło nie tak");
            }
        }
    }

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
