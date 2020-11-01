using System.Collections.Generic;

namespace WebsitePerfomanceManager.Business.Models
{
    public class TestingPageModel
    {
        public TestingPageModel()
        {
            Speeds = new List<PageSpeedModel>();
        }
        public int Id { get; set; }
        public double CurrentResponseTime { get; set; }
        public string PageUrl { get; set; }
        public double MinResponseTime { get; set; }
        public double MaxResponseTime { get; set; }
        public List<PageSpeedModel> Speeds { get; set; }
        public int IdSite { get; set; }
        public SiteModel Site { get; set; }
    }
}
