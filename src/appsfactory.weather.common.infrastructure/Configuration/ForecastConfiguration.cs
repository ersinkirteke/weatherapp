using Appsfactory.Weather.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appsfactory.Weather.Infrastructure.Configuration
{
    public class ForecastConfiguration : IEntityTypeConfiguration<Forecast>
    {
        public void Configure(EntityTypeBuilder<Forecast> builder)
        {
            builder.HasKey(o => o.Id);

            builder.OwnsOne(m => m.Address, a =>
            {
                a.Property(p => p.City).HasMaxLength(150)
                    .HasColumnName("City")
                    .HasDefaultValue("");
                a.Property(p => p.ZipCode).HasMaxLength(12)
                    .HasColumnName("ZipCode")
                    .HasDefaultValue("");
            });

            builder.OwnsOne(m => m.Temperature, a =>
            {
                a.OwnsOne(p => p.Scale, a =>
                {
                    a.Property(p => p.ScaleUnit).HasMaxLength(3)
                    .HasColumnName("ScaleUnit")
                    .HasDefaultValue("");
                });

                a.Property(p => p.Value)
                    .HasColumnName("Temperature");
            });
        }
    }
}
