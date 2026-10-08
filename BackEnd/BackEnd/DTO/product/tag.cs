using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BackEnd.DTO.produc
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateTag
    {
        public string Name { get; set; }
    }

    public class UpdateTag
    {
        public string Name { get; set; }
    }

    public class ListingTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ListingProductTag
    {
        public int Id { get; set; }
        public string TagName { get; set; }
    }

}
