namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class UpdateAppUserDTO
    {
        public int AppUserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public bool IsActive { get; set; }
        public int AppRoleId { get; set; }
    }
}
