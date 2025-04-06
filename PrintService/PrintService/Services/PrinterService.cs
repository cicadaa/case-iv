using PrintService.Database;
using PrintService.Models;

namespace PrintService.Services;

public class PrinterService : IPrinterService
{
    private readonly IPrinterProvider _printerProvider;
    private readonly IPrinterDbAccesor _db;
    private readonly ILogger<PrinterService> _logger;

    public PrinterService(IPrinterProvider printerProvider, IPrinterDbAccesor db, ILogger<PrinterService> logger)
	{
        _printerProvider = printerProvider;
        _db = db;
        _logger = logger;
	}

    public async Task<IEnumerable<Printer>> GetAllPrintersAsync()
    {
        if (await TryReconcilePrinterAsync())
        {
            return await _db.GetAllPrintersAsync();
        }
        return Enumerable.Empty<Printer>();
     }

    public Task<Printer> GetPrinterAsync(string id)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> TryReconcilePrinterAsync()
    {
        
        var vendorPrinters = await _printerProvider.GetAllPrintersAsync();
        var localPrinters = await _db.GetAllPrintersAsync();

        // printers in the vendor list but not in the local database 
        var toAdd = vendorPrinters.Where(vp => !localPrinters.Any(lp => lp.Id == vp.Id));

        // printers in the local database but not in the vendor list 
        var toRemove = localPrinters.Where(lp => !vendorPrinters.Any(vp => vp.Id == lp.Id));

        if (await _db.RemovePrintersAsync(toRemove) && await _db.AddPrintersAsync(toAdd))
        {
            _logger.LogInformation($"Inserting {toAdd.Count()} printers and removing {toRemove.Count()} printers.");
            return true;
        }
        
        _logger.LogCritical("Failed to reconcile printer db");
        return false;
    }
}

