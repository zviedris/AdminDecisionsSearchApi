using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using CsvSearchService.Models;
using CsvSearchService.Services;

namespace AdminDecisionsSearchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsvSearchController : ControllerBase
    {
        private readonly CsvService _csvService;

        public CsvSearchController()
        {
            // Initialize the CSV service (adjust to your actual file path)
            _csvService = new CsvService("data.csv");
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<CsvRecord>> Search([FromQuery] string name, [FromQuery] string nmrCode)
        {
            // Ensure the required query parameters are provided
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(nmrCode))
                return BadRequest(new { message = "Name and NMR Code are required." });

            // Get today's date to check for records in the date range
            DateTime requestDate = DateTime.Now.Date;

            // Call the service to search records
            var results = _csvService.SearchRecords(name, nmrCode, requestDate);

            // If no records are found, return a JSON response with an appropriate message
            if (!results.Any())
            {
                return Ok(new { message = "No records found" });
            }

            // If records are found, return them as JSON
            return Ok(results);
        }
    }
}