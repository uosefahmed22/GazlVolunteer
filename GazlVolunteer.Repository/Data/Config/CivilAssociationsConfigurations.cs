using GazlVolunteer.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazlVolunteer.Repository.Data.Config
{
    public class CivilAssociationsConfigurations : IEntityTypeConfiguration<CivilAssociations>
    {
        public void Configure(EntityTypeBuilder<CivilAssociations> builder)
        {
            builder.Property(x => x.Latitude).HasColumnType("decimal(18,16)");
            builder.Property(x => x.Longitude).HasColumnType("decimal(18,16)");
        }
    }
}
