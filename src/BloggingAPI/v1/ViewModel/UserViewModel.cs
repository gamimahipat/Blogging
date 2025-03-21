namespace BloggingAPI.v1
{
    public class UserSaveViewModel
    {
        public required string UserName { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string MobileNo { get; set; }
        public required string Password { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? Bio { get; set; }
        public int RoleId { get; set; }
    }

    public class LoginViewModel
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
