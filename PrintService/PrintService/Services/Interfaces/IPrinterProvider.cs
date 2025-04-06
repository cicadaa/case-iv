using PrintService.Models;

namespace PrintService.Services;

public interface IPrinterProvider {

    public Task<IEnumerable<Printer>> GetAllPrintersAsync();
}

