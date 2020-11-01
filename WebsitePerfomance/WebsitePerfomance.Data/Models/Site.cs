using System.Collections.Generic;

namespace WebsitePerfomance.Data.Models
{
    public class Site
    {
        public Site()
        {
            Pages = new List<TestingPage>();
        }
        public int Id { get; set; }
        public string Url { get; set; }
        public string Sitemap { get; set; }
        public List<TestingPage> Pages { get; set; }
    }
}
