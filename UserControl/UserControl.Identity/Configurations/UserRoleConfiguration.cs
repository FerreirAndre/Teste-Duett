using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace UserControl.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "c4857c2f-6b68-46f2-9c74-1e02a857ff58",
                    UserId = "a8b43007-2185-4e98-bb16-93e01b472e87"
                },
                new IdentityUserRole<string> {
                    RoleId = "f8b02d1b-12e6-4de2-bb0d-7c5d83a824f1",
                    UserId = "4db21abf-5c9b-404b-9e5f-245b67d6fa7b"
                }
                );
        }
    }
}
