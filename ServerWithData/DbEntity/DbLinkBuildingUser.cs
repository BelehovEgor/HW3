﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWithData.DbEntity
{
    public class DbLinkBuildingUser
    {
        public const string TableName = "LinkTable";

        public Guid Id { get; set; }
        public Guid BuildingId { get; set; }
        public Guid UserId { get; set; }
    }

    public class DbLinkBuildUserConfiguration : IEntityTypeConfiguration<DbLinkBuildingUser>
    {
        public void Configure(EntityTypeBuilder<DbLinkBuildingUser> builder)
        {

            builder.ToTable(DbLinkBuildingUser.TableName);

            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.BuildingId)
                .IsRequired();

            builder
                .Property(u => u.UserId)
                .IsRequired();
        }
    }
}