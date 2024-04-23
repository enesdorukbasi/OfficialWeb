namespace DorukSoft.OfficialWeb.Domain.Entities
{
    public class AppRole
    {
        public int AppRoleId { get; set; }
        public string? Definition { get; set; }
        public List<AppUser>? AppUsers { get; set; }
    }
}
