using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Models
{
    public class IndeedModel
    {
        public int Version { get; set; }
        public string Query { get; set; }
        public string Location { get; set; }
        public int Radius { get; set; }
        public string PaginationPayload { get; set; }
        public bool Dupefilter { get; set; }
        public bool Highlight { get; set; }
        public int TotalResults { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public int PageNumber { get; set; }
        public int EstimatedRevenue { get; set; }
        public List<JobOffer> Results { get; set; }
    }
    public class JobOffer
    {
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string FormattedLocation { get; set; }
        public string Source { get; set; }
        public DateTime Date { get; set; }
        public string Snippet { get; set; }
        public string Url { get; set; }
        public string Latitude { get; set; }
        public float Longitude { get; set; }
        public string Jobkey { get; set; }
        public bool Sponsored { get; set; }
        public bool Expired { get; set; }
        public bool IndeedApply { get; set; }
        public string FormattedLocationFull { get; set; }
        public string FormattedRelativeTime { get; set; }
        public string Stations { get; set; }
        public string RefNum { get; set; }
    }

    public class FinalJobOffer
    {
        public string JobName { get; set; }
        public string JobLocation { get; set; }
        public string JobCompany { get; set; }
        public string JobDesc { get; set; }
        public HashSet<string> Keys { get; set; }
    }
}
