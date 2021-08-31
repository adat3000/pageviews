using System;
using System.Collections.Generic;
using System.Text;

namespace pageviews
{
    class AllHours
    {
        public string Domain_code { get; set; }
        public string Page_title { get; set; }
        public long Count_views { get; set; }
        public long CNT { get; internal set; }
    }
}
