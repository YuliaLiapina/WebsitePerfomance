using System.Collections.Generic;
using System.Web.Mvc;

namespace WebsitePerfomance.Models
{
    public class SiteViewModel
    {
        public SiteViewModel()
        {
            Pages = new List<TestingPageViewModel>();
            XPagesUrl = new List<string>();
            YPageSpeed = new List<double>();
        }
        public int Id { get; set; }
        public string Url { get; set; }
        public string Sitemap { get; set; }
        public double CurrentSpeedResponse { get; set; }
        public int SelectedId { get; set; }
        public List<string> XPagesUrl { get; set; }
        public List<double> YPageSpeed { get; set; }
        public IEnumerable<SelectListItem> Sites { get; set; }
        public List<TestingPageViewModel> Pages { get; set; }        

    }
}