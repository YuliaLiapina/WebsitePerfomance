using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Helpers;
using System.Web.Mvc;

namespace WebsitePerfomance.Models
{
    public class SiteViewModel
    {
        public SiteViewModel()
        {
            Measurements = new List<MeasurementViewModel>();
        }
        public int Id { get; set; }
        public string Url { get; set; }
        public Chart Chart { get; set; }
        public double MinResponseTime { get; set; }
        public double MaxResponseTime { get; set; }
        public int SelectedId { get; set; }
        public IEnumerable<SelectListItem> Sites { get; set; }
        public ICollection<MeasurementViewModel> Measurements { get; set; }
    }
}