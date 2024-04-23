namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class AppUserLoginDTO
    {
        public int AppUserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string FullName { get { return Name + " " + Surname; } }
        public bool IsActive { get; set; }
        public string? Token { get; set; }
        public DateTime? TokenExpireDateTime { get; set; }
        public int AppRoleId { get; set; }
        public AppRoleListDTO? AppRole { get; set; }
    }
}
