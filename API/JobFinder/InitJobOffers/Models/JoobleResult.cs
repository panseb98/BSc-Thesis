using System;
using System.Collections.Generic;
using System.Text;

namespace InitJobOffers.Models
{
    public class JoobleResult
    {
        public int TotalCount { get; set; }
        public List<JoobleJobModel> Jobs { get; set; }
    }
}
