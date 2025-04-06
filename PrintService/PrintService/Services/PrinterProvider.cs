using System.Net.Http;
using System.Net.Http.Json;
using PrintService.Models;

namespace PrintService.Services;

public class PrinterProvider: IPrinterProvider
{
    private readonly HttpClient _httpClient;

    public PrinterProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Printer>> GetAllPrintersAsync()
    {
        var response = await _httpClient.GetAsync("/Printers");
        response.EnsureSuccessStatusCode();

        var vendorPrinters = await response.Content.ReadFromJsonAsync<List<VendorPrinter>>();
        return vendorPrinters?.Select(x => new Printer(x.Model, x.Name)) ?? Enumerable.Empty<Printer>();
    }
}
