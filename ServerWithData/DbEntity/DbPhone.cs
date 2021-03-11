using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.DbEntity
{
    public class DbPhone
    {
        public const string TableName = "Phones";

        public Guid Id { get; set; }
        public Guid? BuildingId { get; set; }
        public string Number { get; set; }

        public DbBuilding Building { get; set; }
    }

    public class DbLandlinePhoneConfiguration : IEntityTypeConfiguration<DbPhone>
    {
        public void Configure(EntityTypeBuilder<DbPhone> builder)
        {

            builder.ToTable(DbPhone.TableName);

            builder.HasKey(p => p.Id);

            builder
                .Property(p => p.Number)
                .HasMaxLength(10)
                .IsRequired();

            builder
                .Property(p => p.BuildingId)
                .IsRequired(false);

            builder
                .HasOne(p => p.Building)
                .WithOne(b => b.Phone);
        }
    }
}
