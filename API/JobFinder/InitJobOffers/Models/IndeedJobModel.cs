using System;
using System.Collections.Generic;
using System.Text;

namespace InitJobOffers.Models
{
    public class IndeedJobModel
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
}
