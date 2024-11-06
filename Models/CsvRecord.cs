using System;

namespace CsvSearchService.Models
{
    public class CsvRecord
    {
        public string? Name { get; set; }
        public string? NmrCode { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTill { get; set; }
        public string? Explanation { get; set; }
    }
}