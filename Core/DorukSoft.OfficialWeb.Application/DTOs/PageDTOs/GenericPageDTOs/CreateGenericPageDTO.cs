using DorukSoft.OfficialWeb.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace DorukSoft.OfficialWeb.Application.DTOs
{
    public class CreateGenericPageDTO
    {
        public string? PageTitle { get; set; }
        public string? PageContent { get; set; }
        public GenericPageType PageType { get; set; }
        public List<IFormFile>? ViewerPageMedias { get; set; }
        public string? ListItemJson { get; set; }
        public List<string>? ListItem { get; set; }
    }
}
