using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.Models;

namespace WaterLogger.Pages;

public class UpdateModel(IConfiguration configuration) : PageModel
{
    private string _connectionString = configuration.GetConnectionString("DefaultConnection");

    [BindProperty] public DrinkingWaterModel DrinkingWater { get; set; }

    public IActionResult OnGet(int id)
    {
        DrinkingWater = GetById(id);

        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText =
            "UPDATE drinking_water SET date = @Date, quantity = @Quantity, unit = @Unit WHERE id = @id";
        command.Parameters.AddWithValue("@Date", DrinkingWater.Date);
        command.Parameters.AddWithValue("@Quantity", DrinkingWater.Quantity);
        command.Parameters.AddWithValue("@id", DrinkingWater.Id);
        command.Parameters.AddWithValue("@Unit", DrinkingWater.Unit);
        command.ExecuteNonQuery();
        connection.Close();

        return RedirectToPage("./Index");
    }

    private DrinkingWaterModel GetById(int id)
    {
        var data = new DrinkingWaterModel();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM drinking_water WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);

        var reader = command.ExecuteReader();

        if (!reader.Read()) return data;

        data.Id = reader.GetInt32(0);
        data.Date = reader.GetDateTime(1);
        data.Quantity = reader.GetFloat(2);
        data.Unit = reader.GetString(3);

        connection.Close();
        return data;
    }
}