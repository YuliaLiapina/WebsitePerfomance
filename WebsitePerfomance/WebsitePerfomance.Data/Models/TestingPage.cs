using System.Collections.Generic;

namespace WebsitePerfomance.Data.Models
{
    public class TestingPage
    {
        public TestingPage()
        {
            Speeds = new List<PageSpeed>();
        }
        public int Id { get; set; }
        public double CurrentResponseTime { get; set; }
        public string PageUrl { get; set; }
        public double MinResponseTime { get; set; }
        public double MaxResponseTime { get; set; }
        public List<PageSpeed> Speeds { get; set; }
        public int IdSite { get; set; }
        public Site Site { get; set; }
    }
}
