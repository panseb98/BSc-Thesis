using Database;
using Database.Models;
using Database.Models.InitJobOffers;
using HtmlAgilityPack;
using InitJobOffers.Models;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace InitJobOffers.Services
{
    public class InitJobs : IInitJobs
    {
        private uint base1 = 16777619;
        private int prime = 101;


        private readonly DataContext _dbContext;
        public InitJobs(DataContext dbContext = null)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> AddIndeedJobs(string keywords = "", string location = "")
        {
            string generateUrl()
            {
                var keyword = keywords.Replace(" ", "+");
                return $"https://api.indeed.com/ads/apisearch?publisher=8723412683614837&q={keyword}&l=&sort=&radius=&st=&jt=&start=25&limit=100&fromage=&filter=&latlong=1&co=pl&chnl=&userip=1.2.3.4&useragent=Mozilla/%2F4.0%28Firefox%29&v=2&latlong=1&format=json";
            }

            async Task<string> getDesc(string url)
            {
                var client = new System.Net.Http.HttpClient();
                var content = await client.GetStringAsync(url);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(content);
                var tag = doc.GetElementbyId("jobDescriptionText");
                return await DecodeHtmlDescription(tag.InnerText);
            }
            HttpClient _httpClient = new HttpClient();

            try
            {
                var asd = await _httpClient.GetStringAsync(generateUrl());

                var result = JsonConvert.DeserializeObject<IndeedResult>(asd);

                foreach (var item in result.Results)
                {
                    if(_dbContext.JobOffers.Count(x => x.JobKey == item.Jobkey && x.JobFrom == "Indeed").Equals(0))
                    {
                        var desc = await getDesc(item.Url);
                       // _dbContext.Add(new JobOffer() { JobKey = item.Jobkey, JobCompany = item.Company, JobDesc = desc, JobFrom = "Indeed", JobLocation = item.City, JobName = item.JobTitle });
                      //  await _dbContext.SaveChangesAsync();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<Either<Exception,bool>> AddJoobleJobs(string keywords = "", string location = "")
        {
            try
            {
                List<string> indeedSource = new List<string>() { "indeed", "indeed.com" };

                async Task<string> getDesc(string url2)
                {
                    try
                    {
                        var client = new System.Net.Http.HttpClient();
                        var content = await client.GetStringAsync(url2);

                        HtmlDocument doc = new HtmlDocument();
                        doc.LoadHtml(content);
                        var a = doc.DocumentNode;

                        var a3 = doc.DocumentNode.SelectNodes("//div[contains(@class, 'a708d')]")[0].InnerText;
                        return await DecodeHtmlDescription(a3);
                    }
                    catch(Exception e)
                    {
                        return "";
                    }

                }

              

                    List<string> list = new List<string>();

                    var key = await _dbContext.AdminKeys.FirstOrDefaultAsync(x => x.KeyType == "Jooble");

                    var url = "https://pl.jooble.org/api/";

                    WebRequest request = HttpWebRequest.Create(url + key.KeyValue);

                    request.Method = "POST";

                    request.ContentType = "application/json";

                    var writer = new StreamWriter(request.GetRequestStream());

                    writer.Write("{ keywords: '" + keywords + "', location: '" + location + "'}");

                    writer.Close();

                    var response = request.GetResponse();

                    var reader = new StreamReader(response.GetResponseStream());

                    while (!reader.EndOfStream)
                    {
                        list.Add(reader.ReadLine());
                    }
                    StringBuilder builder = new StringBuilder();
                    list.ForEach(cat => builder.Append(cat).Append(""));

                    string result = builder.ToString();
                    try
                    {
                        var resAsOb = JsonConvert.DeserializeObject<JoobleResult>(result);

                        foreach (var item2 in resAsOb.Jobs.Where(x => !((x.Source.Contains("indeed") || x.Source.Contains("indeed")))))
                        {
                            if (_dbContext.JobOffers.Count(x => x.JobKey == item2.Id && x.JobFrom == "Jooble").Equals(0))
                            {
                                var desc = await getDesc(item2.Link);
                                if(desc != "")
                                {
                                    _dbContext.JobOffers.Add(new JobOffer() { JobCompany = item2.Company, JobKey = item2.Id, JobDesc = desc, JobFrom = "Jooble", JobLocation = item2.Location, JobName = item2.Title });
                                    await _dbContext.SaveChangesAsync();
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return new Exception("Coś poszło nie tak :(");
                    }
                

                return true;
            }
            catch(Exception e)
            {
                return new Exception("Coś poszło nie tak :(");
            }
            
        }
        private Dictionary<int, List<string>> GetGroupedKeys(List<string> keys)
        {
            var res = keys.Select(x => x.Length).Distinct();
            Dictionary<int, List<string>> keyValuePairs = new Dictionary<int, List<string>>();
            foreach (var item in res)
            {
                keyValuePairs.Add(item, keys.Where(x => x.Length == item).ToList());
            }

            return keyValuePairs;
        }
        public async Task<List<string>> FullMultipleRabinKarp(List<string> patterns, string text)
        {
            var keyValuePairs = GetGroupedKeys(patterns);

            var finalList = new List<string>();

            foreach (var key in keyValuePairs)
            {
                var keys = Search(text, key.Value, key.Key);
                finalList.AddRange(keys);
            }
            return finalList;
        }
        public async Task<Either<Exception, bool>> UpdateSkillsOnJobs()
        {
            try
            {
                var skills = await _dbContext.JobSkills.Select(x => x.Key.ToLower()).ToListAsync();
                var keyValuePairs = GetGroupedKeys(skills);

                foreach (var item in _dbContext.JobOffers)
                {
                    var finalList = new List<string>();

                    foreach (var key in keyValuePairs)
                    {
                        var keys = Search(item.JobDesc.ToLower(), key.Value, key.Key);
                        finalList.AddRange(keys);
                    }
                    var joinedSkills = string.Join(";", finalList);
                    item.Keys = joinedSkills;
                }

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch(Exception e)
            {
                return new Exception("Coś poszło nie tak :(");
            }
        }
        private async Task<string> DecodeHtmlDescription(string htmlDesc)
        {
            StringWriter myWriter = new StringWriter();

            HttpUtility.HtmlDecode(htmlDesc, myWriter);

            return myWriter.ToString();
        }

        public double hash(string value)
        {
            char[] arrayValue = value.ToCharArray();
            double hash = 0;
            for (int i = 0; i < arrayValue.Length; i++)
            {
                hash += arrayValue[i] * Math.Pow(prime, i);
            }
            return hash;
        }
        public List<string> Search(string txt, List<string> patterns, int length)
        {
            return SearchByRabinKarp(txt, patterns, length);

        }
        public Dictionary<double, string> hashPatterns(List<string> patterns, int l)
        {
            var m = new Dictionary<double, string>();
            foreach (var item in patterns)
            {
                var h = hash(item);
                if (!m.ContainsKey(h))
                    m.Add(h, item);
                
            }
            return m;
        }
        private double recalculateHash(char[] str, int oldIndex, int newIndex, double oldHash, int patternLen)
        {
            double newHash = oldHash - str[oldIndex];
            newHash = newHash / prime;
            newHash += str[newIndex] * Math.Pow(prime, patternLen - 1);
            return newHash;
        }
        public List<string> SearchByRabinKarp(string txt, List<string> patterns, int length)
        {
            var textLength = txt.Length;
            var patternLength = length;
            var matches = new List<string>();

            if (textLength < patternLength || patterns.Count.Equals(0))
            {
                return matches;
            }

            var hashedPatterns = hashPatterns(patterns, patternLength);
            var textHash = hash(txt.Slice(0, patternLength));
            for (int i = 0; i < textLength - patternLength + 1 && hashedPatterns.Count > 0; i++)
            {
                if (i > 0)
                {
                    textHash = recalculateHash(txt.ToCharArray(), i - 1, i + patternLength - 1, textHash, patternLength);
                }
                if (hashedPatterns.ContainsKey(textHash))
                {
                    var a = hashedPatterns.First(x => textHash == x.Key);
                    matches.Add(a.Value);
                }
            }
            return matches.Distinct().ToList();
        }
        public async Task<IEnumerable<SimpleSkillVM>> SearchSkills(string key)
        {
            if((key != "") || key != null)
            {
                return _dbContext.JobSkills.Select(x => new SimpleSkillVM() { Id = x.Id, KeyName = x.Key });
            }

            return _dbContext
                .JobSkills
                .Where(x => x.Key.Contains(key))
                .Select(x => new SimpleSkillVM() { Id = x.Id, KeyName = x.Key });
        }

        public async Task<Either<Exception, bool>> AddJobsByRequest(int recordId)
        {
            var dbOb = await _dbContext.JobRequests.FirstOrDefaultAsync(x => x.Id == recordId);
            var res = await AddJoobleJobs(dbOb.JobName, dbOb.Location);
            _dbContext.JobRequests.Remove(dbOb);
            await _dbContext.SaveChangesAsync();
            return res;
        }
    }
    public static class Extensions
    {
        public static string Slice(this string source, int start, int end)
        {
            if (end < 0)
            {
                end = source.Length + end;
            }
            int len = end - start;
            return source.Substring(start, len);
        }
    }

}
