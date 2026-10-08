using BackEnd.Models.users;

namespace BackEnd.DTO.user
{
    public class User
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public UserRole Role { get; set; }
        public string StatusName { get; set; }
    }

    public class UpdateProfile
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    public class UserStatus
    {
        public int StatusId { get; set; }
    }

    public class UserPassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }


}
