using DorukSoft.OfficialWeb.Domain.ProgramEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DorukSoft.OfficialWeb.Persistence.Configuration.ProgramEntityConfigurations
{
    public class ContactAfterChatMessagesConfiguration : IEntityTypeConfiguration<ContactAfterChatMessage>
    {
        public void Configure(EntityTypeBuilder<ContactAfterChatMessage> builder)
        {
            builder.HasKey(x => x.ContactAfterChatMessageId);
            builder.Property(x => x.Message).IsRequired();
            builder.Property(x => x.ContactId).IsRequired();
        }
    }
}
