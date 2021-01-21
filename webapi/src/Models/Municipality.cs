using System;
namespace webapi.Models {

    public class Municipality {
        public int Id { get; set; }
        public string Municipality_name { get; set; }
        public DateTime Period_start { get; set; }
        public DateTime Period_end { get; set; }
        public double? Yearly { get; set; }
        public double? Monthly { get; set; }
        public double? Weekly { get; set; }
        public double? Daily { get; set; }
    }
}