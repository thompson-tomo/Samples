using System.Data.Common;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Steeltoe.Connectors;
using Steeltoe.Connectors.MySql;
using Steeltoe.Samples.MySql.Models;

namespace Steeltoe.Samples.MySql.Controllers;

public sealed class HomeController(ConnectorFactory<MySqlOptions, MySqlConnection> connector) : Controller
{
    private readonly Connector<MySqlOptions, MySqlConnection> _connector = connector.Get();

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        // Steeltoe: Fetch data from MySQL table.
        var model = new MySqlViewModel
        {
            ConnectionString = _connector.Options.ConnectionString
        };

        await using MySqlConnection connection = _connector.GetConnection();
        await connection.OpenAsync(cancellationToken);
        var command = new MySqlCommand("SELECT * FROM TestData;", connection);
        await using DbDataReader reader = await command.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            string idValue = reader[0].ToString()!;
            string? textValue = reader[1].ToString();

            model.Rows.Add(idValue, textValue);
        }

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
