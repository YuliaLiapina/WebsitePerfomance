using System;
using System.Collections.Generic;

namespace WebsitePerfomance.Models
{
    public class TestingPageViewModel
    {
        public TestingPageViewModel()
        {
            Speeds = new List<PageSpeedViewModel>();
        }
        public int Id { get; set; }
        public string PageUrl { get; set; }
        public DateTime Date { get; set; }
        public double CurrentResponseTime { get; set; }
        public int IdSite { get; set; }
        public SiteViewModel Site { get; set; }
        public double MinResponseTime { get; set; }
        public double MaxResponseTime { get; set; }
        public List<PageSpeedViewModel> Speeds { get; set; }
    }
}