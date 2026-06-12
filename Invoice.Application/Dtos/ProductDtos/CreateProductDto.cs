
namespace Invoice.Application.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        [Required(ErrorMessage = "وارد کردن نام محصول الزامی است.")]
        [StringLength(100, ErrorMessage = "نام محصول نمی‌تواند بیشتر از ۱۰۰ کاراکتر باشد.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "وارد کردن قیمت الزامی است.")]
        [Range(1, int.MaxValue, ErrorMessage = "قیمت محصول باید بزرگتر از صفر باشد.")]
        public required int Price { get; set; }

        [Required(ErrorMessage = "وارد کردن تعداد الزامی است.")]
        [Range(1, int.MaxValue, ErrorMessage = "تعداد محصول نمی‌تواند عدد منفی باشد.")]
        public required int Quantity { get; set; }

        public IFormFile? Image { get; set; }

    }
}
