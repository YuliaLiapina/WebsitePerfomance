using System;

namespace WebsitePerfomance.Data.Models
{
    public class Measurement
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double ResponseTime { get; set; }
        public int IdSite { get; set; }
        public Site Site { get; set; }
    }
}
