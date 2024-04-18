using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.Models;

namespace WaterLogger.Pages
{
    public class CreateModel(IConfiguration configuration) : PageModel
    {
        private readonly  string _connectionString = configuration.GetConnectionString("DefaultConnection");

        [BindProperty]
        public DrinkingWaterModel DrinkingWater { get; set; }

        public IActionResult OnGet(int id)
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO drinking_water (date, quantity, unit) VALUES (@Date, @Quantity, @Unit)";
            command.Parameters.AddWithValue("@Date", DrinkingWater.Date);
            command.Parameters.AddWithValue("@Quantity", DrinkingWater.Quantity);
            command.Parameters.AddWithValue("@Unit", DrinkingWater.Unit);
            command.ExecuteNonQuery();

            connection.Close();
            return RedirectToPage("./Index");
        }
    }
}
