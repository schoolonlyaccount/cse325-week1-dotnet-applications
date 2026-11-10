using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

var currentDirectory = Directory.GetCurrentDirectory();
var storesDirectory = Path.Combine(currentDirectory, "stores");

var salesTotalDir = Path.Combine(currentDirectory, "salesTotalDir");
Directory.CreateDirectory(salesTotalDir);

var salesFiles = FindFiles(storesDirectory);

var salesTotal = CalculateSalesTotal(salesFiles);

File.AppendAllText(Path.Combine(salesTotalDir, "totals.txt"), $"{salesTotal}{Environment.NewLine}");

// Override the existing totals.txt file with the new sales summary report
GenerateSalesSummaryReport(salesFiles);

IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();

    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);

    foreach (var file in foundFiles)
    {
        // The file name will contain the full path, so only check the end of it
        var extension = Path.GetExtension(file);
        if (extension == ".json")
        {
            salesFiles.Add(file);
        }
    }

    return salesFiles;
}

double CalculateSalesTotal(IEnumerable<string> salesFiles)
{
    double salesTotal = 0;

    // Loop over each file path in salesFiles
    foreach (var file in salesFiles)
    {
        // Read the contents of the file
        string salesJson = File.ReadAllText(file);

        // Parse the contents as JSON
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        // Add the amount found in the Total field to the salesTotal variable
        salesTotal += data?.Total ?? 0;
    }

    return salesTotal;
}

void GenerateSalesSummaryReport(IEnumerable<string> salesFiles)
{
    List<string> details = new List<string>();
    double salesTotal = 0;

    foreach (var file in salesFiles)
    {
        var salesJson = File.ReadAllText(file);

        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);

        var folder = Path.GetFileName(Path.GetDirectoryName(file));
        var filename = Path.GetFileName(file);

        details.Add($"{folder}/{filename}: {(data?.Total ?? 0).ToString("C")}");

        salesTotal += data?.Total ?? 0;
    }

    var path = Path.Combine(salesTotalDir, "totals.txt");

    string report = $"""
        Sales Summary
        ----------------------------
         Total Sales: {salesTotal.ToString("C")}
             
         Details:
          {string.Join(Environment.NewLine + "  ", details)}
        """;

    File.WriteAllText(path, report);
}

record SalesData(double Total);