using System;
using System.Collections.Generic;
using System.Text;

namespace InitJobOffers.Models
{
    public class JoobleJobModel
    {
        public string Title { get; set; }
        public string Location { get; set; }
        public string Snippet { get; set; }
        public string Salary { get; set; }
        public string Source { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public string Company { get; set; }
        public DateTime Updated { get; set; }
        public string Id { get; set; }
    }
}
