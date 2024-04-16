using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.Models;

namespace WaterLogger.Pages;

public class IndexModel(IConfiguration configuration) : PageModel
{
    public List<DrinkingWaterModel> Records { get; set; }

    private string _connectionString = configuration.GetConnectionString("DefaultConnection");

    public void OnGet()
    {
        Records = GetAllRecords();
    }

    private List<DrinkingWaterModel> GetAllRecords()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM drinking_water";
        var data = new List<DrinkingWaterModel>();
        using var reader = command.ExecuteReader();

        while (reader.Read())
            data.Add(new DrinkingWaterModel
            {
                Id = reader.GetInt32(0),
                Date = reader.GetDateTime(1),
                Quantity = reader.GetFloat(2),
                Unit = reader.GetString(3)
            });

        connection.Close();
        return data;
    }
}