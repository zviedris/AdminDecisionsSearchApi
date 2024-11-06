using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CsvSearchService.Models;

namespace CsvSearchService.Services
{
    public class CsvService
    {
        private List<CsvRecord> _records;

        public CsvService(string filePath)
        {
            LoadCsvData(filePath);
        }

        private void LoadCsvData(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true
            });
            _records = csv.GetRecords<CsvRecord>().ToList();
        }

        public IEnumerable<CsvRecord> SearchRecords(string name, string nmrCode, DateTime requestDate)
        {
            return _records.Where(record =>
                record.Name.Equals(name, StringComparison.OrdinalIgnoreCase) &&
                record.NmrCode.Equals(nmrCode, StringComparison.OrdinalIgnoreCase) &&
                record.DateFrom <= requestDate &&
                record.DateTill >= requestDate
            );
        }
    }
}