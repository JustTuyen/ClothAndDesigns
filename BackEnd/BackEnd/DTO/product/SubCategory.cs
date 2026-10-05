namespace BackEnd.DTO.product
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
        public string CategoryaName { get; set; }
        public string StatusName { get; set; }
    }

    public class SubCatListing
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class CreateSubCate
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Slug { get; set; }
        public int CategoryId { get; set; }
    }

    public class UpdateInfro
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Slug { get; set; }
    }

    public class UpdateSta
    {
        public int StatusId { get; set; }
    }

    public class UpdateCategory
    {
        public int categoryId { get; set; }
    }

    public class SubCatResultDTO
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Name { get; set; }
    }
}
