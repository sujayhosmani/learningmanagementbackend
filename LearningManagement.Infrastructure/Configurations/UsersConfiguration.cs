using LearningManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningManagement.Infrastructure.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("pk_user_id");

            builder.ToTable("users");

            builder.HasIndex(e => e.EmailId).IsUnique();

            builder.Property(x => x.EmailId)
                .IsRequired()
                .HasColumnName("email");

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(x => x.Password)
                .IsRequired().HasColumnName("password");

            builder.Property(e => e.Role)
               .IsRequired().HasColumnName("role");

            builder.Property(e => e.CreatedBy)
                .IsRequired().HasColumnName("created_by");

            builder.Property(e => e.UpdatedBy)
               .HasColumnName("updated_by");

            builder.Property(e => e.CreatedTimestamp)
               .IsRequired().HasColumnName("create_timestamp");

            builder.Property(e => e.UpdatedTimestamp)
               .HasColumnName("updated_timestamp");

        }
    }
}
