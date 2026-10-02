namespace BackEnd.DTO.other
{
    public class Status
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class CreateStatus
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
    
    public class UpdateStatus
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
