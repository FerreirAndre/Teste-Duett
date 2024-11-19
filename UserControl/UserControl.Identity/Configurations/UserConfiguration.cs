using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserControl.Identity.Models;

namespace UserControl.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            
            builder.HasData(
                new ApplicationUser
                {
                    Id = "a8b43007-2185-4e98-bb16-93e01b472e87",
                    Email = "admin@duett.com",
                    NormalizedEmail = "ADMIN@DUETT.COM",
                    Name = "Admin Duett",
                    Cpf = "123.456.789-10",
                    UserName = "Admin_duett",
                    NormalizedUserName = "ADMIN_DUETT",
                    PasswordHash = hasher.HashPassword(null, "$Admin1"),
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "4db21abf-5c9b-404b-9e5f-245b67d6fa7b",
                    Email = "user@duett.com",
                    NormalizedEmail = "USER@DUETT.COM",
                    Name = "Normal User",
                    Cpf = "000.000.000-00",
                    UserName = "NormalUser",
                    NormalizedUserName = "NORMAL_USER",
                    PasswordHash = hasher.HashPassword(null, "$User1"),
                    EmailConfirmed = true
                }
            );
        }
    }
}
