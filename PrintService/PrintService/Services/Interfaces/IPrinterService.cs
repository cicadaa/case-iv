using PrintService.Models;

namespace PrintService.Services;

public interface IPrinterService
{
    public Task<Printer> GetPrinterAsync(string id);

    public Task<IEnumerable<Printer>> GetAllPrintersAsync();
}

