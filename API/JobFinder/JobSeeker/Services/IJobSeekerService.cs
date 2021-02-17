using JobSeeker.Models;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobSeeker.Services
{
    public interface IJobSeekerService
    {
        Task<Either<Exception, List<RecommendedJobModel>>> GetRecommendedJobs(int userId);
        Task<Either<Exception, bool>> UpdateExpendedUserData(ExpendedUserData model);
        Task<Either<Exception, ExpendedUserData>> GetExpendedUserData(int userId);
        Task<Either<Exception, List<SkillUser>>> GetSkillsDropdownlist(string key);
        Task<Either<Exception, List<string>>> GetAllSkills();
        Task<Either<Exception, List<string>>> GetJobNames(string key);
        Task<Either<Exception, bool>> AddNewJobRequest(NewJob model);
        Task<Either<Exception, bool>> AddNewKeyRequest(NewKey model);
        Task<Either<Exception, RequestList>> GetAllRequests();
        Task<Either<Exception, bool>> AddNewKey(int recordId);
        Task<Either<Exception, bool>> RemoveNewKey(int recordId);
        Task<Either<Exception, bool>> RemoveNewJob(int recordId);
        Task<Either<Exception, List<ExperienceUser>>> GetJobsFromCV(int userId);

        

    }
}
