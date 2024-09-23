﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zeus.Demo.Core.Models;

namespace Zeus.Demo.EntityFrameworkCore.EntityConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Product)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(c => c.ProductId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasOne(c => c.User)
                   .WithMany(c => c.Orders)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }
}
