using PrintService.Models;

namespace PrintService.Database;

public interface IPrinterDbAccesor
{
    public Task<IEnumerable<Printer>> GetAllPrintersAsync();

    public Task<bool> RemovePrintersAsync(IEnumerable<Printer> printers);

    public Task<bool> AddPrintersAsync(IEnumerable<Printer> printers);
}

