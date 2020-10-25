using System.Collections.Generic;

namespace WebsitePerfomanceManager.Business.Models
{
    public class SiteModel
    {
        public SiteModel()
        {
            Measurements = new List<MeasurementModel>();
        }
        public int Id { get; set; }
        public string Url { get; set; }
        public IList<MeasurementModel> Measurements { get; set; }
        public double MinResponseTime { get; set; }
        public double MaxResponseTime { get; set; }
    }
}
