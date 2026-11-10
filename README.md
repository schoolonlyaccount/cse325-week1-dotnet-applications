# Evidence from the end of the module "Create a web API with ASP.NET Core controllers" showing the existing pizzas and your additional pizza.
View lines 13-15 in "Learning-Module-6/Services/PizzaService.cs", line 15 is my added pizza.
```csharp
new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true },
new Pizza { Id = 3, Name = "Hawaii", IsGlutenFree = false } // Added an additional pizza to the seed data
```

# For each operation (GET, POST, PUT, and DELETE), the request you sent, the response you received, and the returned status code.
## GET
- **Request sent:**
```http
GET {{Learning_Module_6_HostAddress}}/pizza/
Accept: application/json

###
```
- **Response received:**
```http
HTTP/1.1 200 OK
Connection: close
Content-Type: application/json; charset=utf-8
Date: Tue, 10 Nov 2026 06:43:15 GMT
Server: Kestrel
Transfer-Encoding: chunked

[
  {
    "id": 1,
    "name": "Classic Italian",
    "isGlutenFree": false
  },
  {
    "id": 2,
    "name": "Veggie",
    "isGlutenFree": true
  },
  {
    "id": 3,
    "name": "Hawaii",
    "isGlutenFree": false
  }
]
```
- **Returned status code:** `200 OK`

## POST
- **Request sent:**
```http
POST {{Learning_Module_6_HostAddress}}/pizza/
Content-Type: application/json

{
    "name": "Chicken BBQ",
    "isGlutenFree": false
}

###
```
- **Response received:**
```http
HTTP/1.1 201 Created
Connection: close
Content-Type: application/json; charset=utf-8
Date: Tue, 10 Nov 2026 06:55:26 GMT
Server: Kestrel
Location: http://localhost:5105/Pizza/4
Transfer-Encoding: chunked

{
  "id": 4,
  "name": "Chicken BBQ",
  "isGlutenFree": false
}
```
- **Returned status code:** `201 Created`

## PUT
- **Request sent:**
```http
PUT {{Learning_Module_6_HostAddress}}/pizza/4
Content-Type: application/json

{
    "id": 4,
    "name": "Pork BBQ",
    "isGlutenFree": false
}

###
```
- **Response received:**
```http
HTTP/1.1 204 No Content
Connection: close
Date: Tue, 10 Nov 2026 06:55:51 GMT
Server: Kestrel


```
- **Returned status code:** `204 No Content`

## DELETE
- **Request sent:**
```http
DELETE {{Learning_Module_6_HostAddress}}/pizza/4

###
```
- **Response received:**
```http
HTTP/1.1 204 No Content
Connection: close
Date: Tue, 10 Nov 2026 06:59:03 GMT
Server: Kestrel


```
- **Returned status code:** `204 No Content`

# A text copy of your working sales summary function from the Work with files and directories in a .NET app module.
```csharp
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
```