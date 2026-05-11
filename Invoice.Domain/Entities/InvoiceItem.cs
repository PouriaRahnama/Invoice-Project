namespace Invoice.Domain.Entities
{
    public class InvoiceItem : BaseEntity
    {
        public Guid InvoiceId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } // تعداد محصول در فاکتور
        public long UnitPrice { get; set; } // قیمت واحد در زمان صدور فاکتور
        public long TotalPrice { get; set; } // قیمت کل این آیتم


        #region Navigation
        public Invoice Invoice { get; set; }
        public Product Product { get; set; }
        #endregion
    }
}
