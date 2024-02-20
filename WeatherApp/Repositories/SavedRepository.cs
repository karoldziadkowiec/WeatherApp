using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WeatherApp.Database;
using WeatherApp.Models.DTOs;
using WeatherApp.Models.Entities;

namespace WeatherApp.Repositories
{
    public class SavedRepository : ISavedRepository
    {
        private readonly AppDbContext _context;

        public SavedRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<SavedWeather>> GetAllSaved()
            => await Task.FromResult(_context.Saved.OrderByDescending(s => s.Id));

        public async Task<SavedWeather> GetSavedWeather(int weatherId)
            => await _context.Saved.FirstOrDefaultAsync(s => s.Id == weatherId);

        public async Task SaveWeather(WeatherData weatherData)
        {
            try
            {
                var weather = new SavedWeather
                {
                    WeatherId = weatherData.Id
                };

                _context.Saved.Add(weather);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while saving weather: {ex.Message}");
            }
        }

        public async Task RemoveWeather(int weatherId)
        {
            var weather = await _context.Saved.FirstOrDefaultAsync(s => s.Id == weatherId);
            if (weather != null)
            {
                _context.Saved.Remove(weather);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<byte[]> GetSavedCsvBytes()
        {
            var savedItems = await _context.Saved.ToListAsync();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("SavedWeather");

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
                foreach (var savedItem in savedItems)
                {
                    var weather = await _context.History.FindAsync(savedItem.WeatherId);
                    if (weather != null)
                    {
                        worksheet.Cell(row, 1).Value = savedItem.Id;
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
                }

                using (var memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    memoryStream.Position = 0;
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
