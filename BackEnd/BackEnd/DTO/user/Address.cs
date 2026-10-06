namespace BackEnd.DTO.user
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string? Note { get; set; }
        public bool IsDefault { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string StatusName { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string wardName { get; set; }
        public string FullName { get; set; }
    }

    public class CreateAddress
    {
        public int UserId { get; set; }
        public int StatusId { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public string Street { get; set; }
        public bool IsDefault { get; set; }
        public string? Note { get; set; }
    }

    public class UpdateAddress
    {
        public string Street { get; set; }
        public string? Note { get; set; }
        public bool IsDefault { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
    }

    public class AddressStatus
    {
        public int StatusId { get; set; }
    }

    public class AddressListing
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string? Note { get; set; }
        public bool IsDefault { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string wardName { get; set; }
        public string FullName { get; set; }
    }
    //CITY
    public class City
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<DistrictListing> Districts { get; set; } = new();
    }

    public class CreateCity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class UpdateCity
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class CityListing
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public List<DistrictListing> Districts { get; set; } = new();
    }


    //District
    public class District
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CityName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<WardListing> Wards { get; set; } = new();
    }

    public class UpdateDistrict
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int CityId { get; set; }
    }

    public class CreateDistrict
    {
        public int CityId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class DistrictListing
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public List<WardListing> Wards { get; set; } = new();
    }


    //ward
    public class Ward
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DistrictName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class CreateWard
    {
        public int DistrictId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
    public class UpdateWard
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int DistrictId { get; set; }
    }

   
    public class WardListing
    {
        public int Id { get; set; }
        public string Name { set; get; }
    }

    //
    public class Result
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
