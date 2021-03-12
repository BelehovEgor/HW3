using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.DbEntity
{
    public class DbBuilding
    {
        public const string TableName = "Builds";

        public Guid Id { get; set; }
        public Guid? OwnerId { get; set; }
        public Guid? PhoneId { get; set; }
        public string Address { get; set; }

        public DbUser Owner { get; set; }
        public DbPhone Phone { get; set; }
        public DbLinkBuildingUser Link { get; set; }
    }

    public class DbHouseConfiguration : IEntityTypeConfiguration<DbBuilding>
    {
        public void Configure(EntityTypeBuilder<DbBuilding> builder)
        {

            builder.ToTable(DbBuilding.TableName);

            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.OwnerId)
                .IsRequired(false);

            builder
                .Property(u => u.PhoneId)
                .IsRequired(false);

            builder
                .Property(u => u.Address)
                .HasMaxLength(40)
                .IsRequired();

            builder
                .HasOne(b => b.Owner)
                .WithMany(u => u.Buildings)
                .HasForeignKey(b => b.OwnerId);

            builder
                .HasOne(b => b.Phone)
                .WithOne(p => p.Building)
                .HasForeignKey<DbPhone>(b => b.BuildingId);

            builder
                .HasOne(b => b.Link)
                .WithOne(l => l.Building);

        }
    }
}
