namespace BackEnd.DTO.product
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string URL { get; set; }
        public string AltText { get; set; }
        public string StatusName { get; set; }
        public List<SubCatListing> SubCategories { get; set; } = new();
    }

    public class ListingCat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string AltText { get; set; }
        public List<SubCatListing> SubCategories { get; set; } = new();
    }
    public class CreateCategory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string AltText { get; set; }

    }

    public class UpdateCat
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateImage
    {
        public IFormFile Image { get; set; }
        public string AltText { get; set; }
    }

    public class CatUpdateStatus
    {
        public int StatusId { get; set; }
    }

    public class CatResultDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
    }
}
