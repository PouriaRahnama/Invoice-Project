namespace Invoice.Web.Controllers;
public class ReportController : ApiBaseController
{
    public ReportController(ILogger<ApiBaseController> logger) : base(logger) { }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult GenerateInvoice()
    {

        var report = new StiReport();

        var rootPath = AppContext.BaseDirectory;
        var reportPath = Path.Combine(rootPath, "wwwroot\\Reports", "Invoice.mrt");
        report.Load(reportPath);

        //todo
        var myData = new
        {
            FirstName = "علی",
            LastName = "محمدی",
        };

        report.RegData("DT", myData);
        report.Render(false);

        using var stream = new MemoryStream();
        report.ExportDocument(StiExportFormat.Pdf, stream);

        return File(stream.ToArray(), "application/pdf", "Invoice.pdf");
    }
}

