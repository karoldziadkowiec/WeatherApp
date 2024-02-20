using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherApp.Database;
using WeatherApp.Models.DTOs;
using WeatherApp.Models.Entities;

namespace WeatherApp.Repositories
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly AppDbContext _context;

        public HistoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<WeatherData>> GetAllHistory()
            => await Task.FromResult(_context.History.OrderByDescending(h => h.Created));

        public async Task<WeatherData> GetWeatherFromHistory(int historyId)
            => await _context.History.FirstOrDefaultAsync(h => h.Id == historyId);

        public async Task<byte[]> GetHistoryCsvBytes()
        {
            var history = await _context.History.ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("WeatherHistory");

                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "City";
                worksheet.Cell(1, 3).Value = "Country";
                worksheet.Cell(1, 4).Value = "Title";
                worksheet.Cell(1, 5).Value = "Description";
                worksheet.Cell(1, 6).Value = "Icon";
                worksheet.Cell(1, 7).Value = "Temperature";
                worksheet.Cell(1, 8).Value = "Pressure";
                worksheet.Cell(1, 9).Value = "Humidity";
                worksheet.Cell(1, 10).Value = "WindSpeed";
                worksheet.Cell(1, 11).Value = "Sunrise";
                worksheet.Cell(1, 12).Value = "Sunset";
                worksheet.Cell(1, 13).Value = "Created";

                var row = 2;
                foreach (var weather in history)
                {
                    worksheet.Cell(row, 1).Value = weather.Id;
                    worksheet.Cell(row, 2).Value = weather.City;
                    worksheet.Cell(row, 3).Value = weather.Country;
                    worksheet.Cell(row, 4).Value = weather.Title;
                    worksheet.Cell(row, 5).Value = weather.Description;
                    worksheet.Cell(row, 6).Value = weather.Icon;
                    worksheet.Cell(row, 7).Value = weather.Temperature;
                    worksheet.Cell(row, 8).Value = weather.Pressure;
                    worksheet.Cell(row, 9).Value = weather.Humidity;
                    worksheet.Cell(row, 10).Value = weather.WindSpeed;
                    worksheet.Cell(row, 11).Value = weather.Sunrise.ToShortDateString();
                    worksheet.Cell(row, 12).Value = weather.Sunset.ToShortDateString();
                    worksheet.Cell(row, 13).Value = weather.Created.ToShortDateString();
                    row++;
                }

                using (var memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.Position = 0;
                    return memoryStream.ToArray();
                }
            }
        }

        public async Task<IEnumerable<WeatherData>> SearchPartial(string searchTerm)
        {
            var searchedWeathers = await _context.History
                .Where(h => h.City.Contains(searchTerm) || h.Country.Contains(searchTerm))
                .ToListAsync();

            return searchedWeathers;
        }
    }
}
