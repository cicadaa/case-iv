namespace PrintService.Models;

public class Printer
{
	public Printer(string model, string name)
	{
		Id = Guid.NewGuid().ToString();
		Model = model;
		Name = name;
	}

	public string Id { get; set; }

	public string Model { get; set; }

	public string Name { get; set; }

	public PrinterMetadata? Metadata { get; set; }
}