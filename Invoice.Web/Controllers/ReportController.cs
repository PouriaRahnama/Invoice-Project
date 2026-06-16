using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Invoice.Web.Controllers;
public class ReportController : ApiBaseController
{
    public ReportController(ILogger<ApiBaseController> logger) : base(logger) { }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GenerateInvoice(
        [FromQuery][Required(ErrorMessage ="شناسه فاکتور الزامی است")] Guid invoiceId)
    {

        var report = new StiReport();

        var rootPath = AppContext.BaseDirectory;
        var reportPath = Path.Combine(rootPath, "wwwroot\\Reports", "Invoice.mrt");
        report.Load(reportPath);

        var myData = new GetInvoiceDetailsReportDto();

        myData = await _invoiceService.GetByIdForReportAsync(invoiceId);

        var ds = BuildInvoiceReportDataSet(myData);

        report.RegData(ds);
        report.Render(false);

        using var stream = new MemoryStream();
        report.ExportDocument(StiExportFormat.Pdf, stream);
        return File(stream.ToArray(), "application/pdf", "Invoice.pdf");
    }

    private DataSet BuildInvoiceReportDataSet(GetInvoiceDetailsReportDto dto)
    {
        var ds = new DataSet();

        // ----- Parent table (DT) -----
        var dtInvoice = new DataTable("DT");
        dtInvoice.Columns.Add("invoiceId", typeof(Guid));
        dtInvoice.Columns.Add("invoiceNumber", typeof(string));
        dtInvoice.Columns.Add("totalPrice", typeof(string));
        dtInvoice.Columns.Add("customerName", typeof(string));
        dtInvoice.Columns.Add("createdDateTime", typeof(string));
        dtInvoice.Columns.Add("address", typeof(string));
        dtInvoice.Columns.Add("phone", typeof(string));
        dtInvoice.Columns.Add("status", typeof(string));

        dtInvoice.Rows.Add(
            dto.InvoiceId,
            dto.InvoiceNumber,
            dto.TotalPrice,
            dto.CustomerName,
            dto.CreatedDateTime,
            dto.Address,
            dto.Phone,
            dto.Status
        );

        ds.Tables.Add(dtInvoice);

        // ----- Child table (Items) -----
        var dtItems = new DataTable("Items");
        dtItems.Columns.Add("invoiceId", typeof(Guid));
        dtItems.Columns.Add("productName", typeof(string));
        dtItems.Columns.Add("quantity", typeof(int));
        dtItems.Columns.Add("unitPrice", typeof(string));
        dtItems.Columns.Add("totalPrice", typeof(string));
        dtItems.Columns.Add("discountPercent", typeof(string));

        foreach (var item in dto.Items)
        {
            dtItems.Rows.Add(
                dto.InvoiceId,
                item.ProductName,
                item.Quantity,
                item.UnitPrice,
                item.TotalPrice,
                item.DiscountPercent
            );
        }

        ds.Tables.Add(dtItems);

        // ----- Relation -----
        ds.Relations.Add(
            "Relation1",
            ds.Tables["DT"].Columns["invoiceId"],
            ds.Tables["Items"].Columns["invoiceId"]
        );

        return ds;
    }

}

