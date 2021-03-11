using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.DbEntity
{
    public class DbUser
    {
        public const string TableName = "Users";

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int? Age { get; set; }

        public ICollection<DbBuilding> Buildings { get; set; } = new HashSet<DbBuilding>();

    }

    public class DbUserConfiguration : IEntityTypeConfiguration<DbUser>
    {
        public void Configure(EntityTypeBuilder<DbUser> builder)
        {
            builder.ToTable(DbUser.TableName);

            builder.HasKey(u => u.Id);
            
            builder
                .Property(u => u.Name)
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(u => u.Lastname)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(u => u.Age)
                .IsRequired(false);

            builder
                .HasMany(u => u.Buildings)
                .WithOne(b => b.Owner);
        }
    }
}
