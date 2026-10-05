namespace BackEnd.Service.media
{
    public interface IImageUploadService
    {
        Task<UploadResult> UploadAsync(IFormFile file);
        Task DeleteAsync(string Key);
    }
    public class UploadResult
    {
        public string Url { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
        //public string AltText { get; set; }
        public long SizeBytes { get; set; }
        public string ContentType { get; set; } = string.Empty;
    }
}
