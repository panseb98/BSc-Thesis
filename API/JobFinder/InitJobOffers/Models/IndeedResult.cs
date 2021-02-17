using System;
using System.Collections.Generic;
using System.Text;

namespace InitJobOffers.Models
{
    public class IndeedResult
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
        public List<IndeedJobModel> Results { get; set; }
    }
}
