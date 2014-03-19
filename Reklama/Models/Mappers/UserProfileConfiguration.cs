using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Domain.Entity.Shared;

namespace Reklama.Models.Mappers
{
    public class UserProfileConfiguration: EntityTypeConfiguration<UserProfile>
    {
        public UserProfileConfiguration()
        {
            ToTable(TableMap.UserProfileTable);
            HasKey(u => u.UserId);
            Property(u => u.Name).HasMaxLength(32).IsOptional().IsUnicode();
            Property(u => u.Surname).HasMaxLength(32).IsOptional().IsUnicode();
            Property(u => u.Email).HasMaxLength(128).IsRequired().IsUnicode();
            Property(u => u.Skype).HasMaxLength(32).IsOptional().IsUnicode();
            Property(u => u.Icq).HasMaxLength(32).IsOptional().IsUnicode();
            Property(u => u.Phone).HasMaxLength(32).IsOptional().IsUnicode();
            Property(u => u.AvatarLink).HasMaxLength(255).IsOptional().IsUnicode();
            Property(u => u.Site).HasMaxLength(128).IsOptional().IsUnicode();
            Property(u => u.About).HasMaxLength(255).IsOptional().IsUnicode();
            Property(u => u.Birthday).IsOptional();
        }
    }
}