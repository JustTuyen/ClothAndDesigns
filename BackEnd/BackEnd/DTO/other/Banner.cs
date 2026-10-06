namespace BackEnd.DTO.other
{
    public class Banner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsThumbnail { get; set; }
        public string? URL { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? StatusName { get; set; }
    }

    public class CreateBanner
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public  string? Note { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsThumbail { get; set; }
        public IFormFile Image { get; set; }
        public string AltText { get; set; }
    }

    public class UpdateBanner
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsThumbail { get; set; }
    }

    public class UpdateImgBanner{
        public IFormFile Image { get; set; }
        public string Alttest { get; set; }
    }

    public class UpdateStatusBanner
    {
        public int StatusId { get; set; }
    }

    public class BannerSilder
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public bool IsThumbnail { get; set; }
        public int DisplayOrder { get; set; }
        public string Name { get; set; }
    }

    public class ResultDTO
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        //public int DisplayOrder { get; set; }
    }
}
