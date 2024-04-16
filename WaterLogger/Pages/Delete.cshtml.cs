using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.Models;

namespace WaterLogger.Pages;

public class DeleteModel(IConfiguration configuration) : PageModel
{
    private string _connectionString = configuration.GetConnectionString("DefaultConnection");

    [BindProperty] public DrinkingWaterModel DrinkingWater { get; set; }

    public IActionResult OnGet(int id)
    {
        DrinkingWater = GetById(id);

        return Page();
    }

    public IActionResult OnPost(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM drinking_water WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);
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

        connection.Close();
        return data;
    }
}