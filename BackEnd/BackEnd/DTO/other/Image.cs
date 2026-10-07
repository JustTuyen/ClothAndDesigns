namespace BackEnd.DTO.other
{

    public class Image
    {
        public int Id { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Type { get; set; } = "image/png";
        public string? AltText { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ImageListing
    {
        public int Id { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string? AltText { get; set; }
    }
    //
    public class ImageGallary
    {
        public int Id { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsThumbnail { get; set; } 
        public DateTime CreatedAt { get; set; }
        public int ProductId { get; set; }
        public int ImageId { get; set; }
        public int? VariationId { get; set; }
    }

    public class ImageGallaryListing
    {
        public int Id { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsThumbnail { get; set; }
        public int ProductId { get; set; }
        public int ImageId { get; set; }
        public int? VariationId { get; set; }
    }

    public class CreateImageGallary
    {
        public int DisplayOrder { get; set; }
        public bool IsThumbnail { get; set; }
        public int ProductId { get; set; }
        public int ImageId { get; set; }
        public int? VariationId { get; set; }
    }
}
