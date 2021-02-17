using InitJobOffers.Models;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InitJobOffers.Services
{
    public interface IInitJobs
    {
        Task<Either<Exception, bool>> AddJoobleJobs(string keywords = "", string location = "");
        Task<Either<Exception, bool>> AddJobsByRequest(int recordId);
        Task<List<string>> FullMultipleRabinKarp(List<string> patterns, string text);
        Task<bool> AddIndeedJobs(string keywords = "", string location = "");
        Task<IEnumerable<SimpleSkillVM>> SearchSkills(string key);
        List<string> Search(string txt, List<string> patterns, int length);
        Task<Either<Exception, bool>> UpdateSkillsOnJobs();

    }
}
