using Authorization.Services;
using Database;
using InitJobOffers.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace xUnitTests
{
    public class UnitTest1
    {
        IInitJobs myService = new InitJobs();
        [Fact]
        public async void TestRabinKarp2()
        {
            List<string> patterns = getKeys();
            patterns = patterns.Select(x => x.ToLower()).ToList();

            string jobDeccription = getJobDesc();
            List<string> joinedPatterns = new List<string>() { "c#", "ios", ".net", "typescript", "elasticsearch" };
            
            var res = await myService.FullMultipleRabinKarp(patterns, jobDeccription.ToLower());
            CollectionAssert.AreEquivalent(joinedPatterns, res);
        }
        private string getJobDesc() => "Must have  English  WebAPI  ASP.NET MVC  C#   Nice to have  Azure  TypeScript  Proactivity  Elasticsearch  Team work  Attention to quality  Curiosity   Work methodology  Integration tests  Yes  Unit tests  Yes  Agile management  Yes  Issue tracking tool  Yes  Knowledge repository  Yes  Code reviews  Yes  Pair programming  Yes  Static code analysis  Yes  Version control system  Yes  Build server  Yes  Commit on the first day  Yes  One command build possible  Yes  Up and running within 2h  Yes  Freedom to choose tools  Yes   Equipment supplied  Operating System  Computer  Notebook  Monitors  up to 2 external   Offer details  Start  ASAP  Contract duration  Permanent contract  Paid holiday  Yes  Part time work  No  Remote possible  Yes  Flexible hours  Yes  Travel involved  some   Perks in the office  Free coffee  Canteen  Bike parking  Shower  Free snacks  Free beverages  Startup atmosphere  No dress code  Kitchen  English lessons  Norwegian lessons   Benefits  Training budget  Private healthcare  Flat structure  Small teams  International projects  In-house trainings  In-house hack days  Conference budget  Team Events ";
        private List<string> getKeys() => new List<string>() { "Affiliate Marketing", "B2B Marketing", "B2C Marketing", "Content Marketing", "Demand Generation", "Eloqua", "Email Marketing", "Event Marketing", "Excel", "Google AdWords", "Google Analytics", "Google Tag Manager", "Marketing Automation", "Marketo", "Paid Social", "Pardot", "Pay Per Click", "Product Marketing", "Programmatic Display Advertising", "Segment", "SEM", "SEO", "Salesforce", "SQL", "Wordpress", "CSS", "HTML", "Javascript", ".NET", "Android ", "Angular", "Angular.js", "AWS", "C#", "C++", "Cassandra", "Chef", "DevOps", "Django", "Docker", "Drupal", "ElasticSearch", "Elixir", "ES6", "Go lang", "Google Cloud", "Hadoop", "iOS", "Java", "Jenkins", "Kafka", "Kotlin", "Kubernetes", "Laravel", "Linux", "Machine Learning", "MapReduce", "Microsoft Azure", "MongoDB", "MySQL", "Node.js", "PHP", "PostgreSQL", "Puppet", "Python", "React.js", "Redis", "Ruby", "Ruby On Rails", "Scala", "Serverless", "Spark", "SQL", "SQL Server", "Swift", "Symphony", "Vue.js", "Graphic Design", "Mobile Design", "Photoshop", "Product Design", "Sketch", "UI Design", "UX Design", "Wireframes", "react", "typescript" };
        [Fact]
        public async void TestFitJobOfferWithCandidate()
        {
            List<string> candidateSkills = new List<string>() { "c#", 
                "ios",
                ".net", 
                "angular", 
                "javascript", 
                "azure" 
            };

            var expectedResult = 60;

            var result = ReturnFittingResult(await myService.FullMultipleRabinKarp(getKeys()
                .Select(x => x.ToLower())
                .ToList(),
                getJobDesc().ToLower()),
                candidateSkills);
            NUnit.Framework.Assert.AreEqual(expectedResult, result);
        }

        private int ReturnFittingResult(List<string> jobSkills, List<string> candidateSkills) => Convert.ToInt32(((double)jobSkills
                        .Intersect(candidateSkills)
                        .ToList()
                        .Count() / (double)jobSkills.Count()) * 100);
    }
}
