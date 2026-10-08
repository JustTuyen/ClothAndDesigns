using BackEnd.DTO.other;
using BackEnd.DTO.produc;

namespace BackEnd.DTO.product
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; } = 0;
        public string Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public string? StatusName { get; set; }
        public string? SubCatName { get; set; }
        //public List<ListingTag> Tags { get; set; } = new();
        public List<ImageGallaryListing> ImageGallaries { get; set; } = new();
    }

    public class CreateProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; } = 0;
        public string Slug { get; set; }
        public int? DiscountId { get; set; }
        public int SubCategoryId { get; set; }
        public List<int> TagIds { get; set; } = new();
        public List<IFormFile> Images { get; set; } = new();
        public string AltText { get; set; }
    }

    public class ListingProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public decimal BasePrice { get; set; } = 0;
        public string ImageURL { get; set; }
        public decimal? DiscountPercentage { get; set; }

    }

    public class UpdateProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; } = 0;
        public string Slug { get; set; }
        public int SubCategoryId { get; set; }
        public List<int> TagIds { get; set; } = new();
    }

    public class ProductDiscount
    {
        public int DiscountId { get; set; }
    }
    public class ProductStatus
    {
        public int StatusId { get; set; }
    }

    public class ProductImages
    {
        public List<IFormFile> Images { get; set; } = new();
        public string AltText { get; set; }
    }

    public class ProductFilter
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }

        public bool? HasDiscount { get; set; }
        public int? MinViewCount { get; set; }

        public string? Keyword { get; set; }
        // "CreatedAt" , "ViewCount" , "Price"
        public string? SortBy { get; set; } = "CreatedAt";
        public bool SortDescending { get; set; } = true;
        //
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class ResultProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
