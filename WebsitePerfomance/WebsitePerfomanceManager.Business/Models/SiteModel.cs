using System.Collections.Generic;

namespace WebsitePerfomanceManager.Business.Models
{
    public class SiteModel
    {
        public SiteModel()
        {
            Pages = new List<TestingPageModel>();
        }
        public int Id { get; set; }
        public string Url { get; set; }
        public string Sitemap { get; set; }
        public List<TestingPageModel> Pages { get; set; }
    }
}
