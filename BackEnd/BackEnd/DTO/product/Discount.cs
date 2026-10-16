using BackEnd.DTO.produc;

namespace BackEnd.DTO.product
{
    public class Discount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Percentage { get; set; }
        public string StatusName { get; set; }
        public string ImgURL { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationInDays { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateDiscount
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Percentage { get; set; }
        public IFormFile? Image { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationInDays { get; set; }
        public string AltText { get; set; }
    }

    public class UpdateDiscount
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Percentage { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationInDays { get; set; }
    }

    public class DiscountStatus
    {
        public int StatusId { get; set; }
    }

    public class DiscountImage
    {
        public IFormFile Image { get; set; }
        public string AltText { get; set; }
    }

    public class DiscountListing
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgURL { get; set; }
    }

    public class ResultDiscount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Percentage { get; set; }
    }
}
