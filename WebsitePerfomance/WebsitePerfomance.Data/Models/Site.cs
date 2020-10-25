using System.Collections.Generic;

namespace WebsitePerfomance.Data.Models
{
    public class Site
    {
        public Site()
        {
            Measurements = new List<Measurement>();
        }
        public int Id { get; set; }
        public string Url { get; set; }
        public double MinResponseTime { get; set; }
        public double MaxResponseTime { get; set; }
        public ICollection<Measurement> Measurements { get; set; }
    }
}
