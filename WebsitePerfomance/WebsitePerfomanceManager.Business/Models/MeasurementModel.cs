using System;

namespace WebsitePerfomanceManager.Business.Models
{
    public class MeasurementModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double ResponseTime { get; set; }
        public int IdSite { get; set; }
        public SiteModel Site { get; set; }
    }
}
