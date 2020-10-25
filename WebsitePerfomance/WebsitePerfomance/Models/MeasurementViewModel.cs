using System;

namespace WebsitePerfomance.Models
{
    public class MeasurementViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double ResponseTime { get; set; }
        public int IdSite { get; set; }
        public SiteViewModel Site { get; set; }
    }
}