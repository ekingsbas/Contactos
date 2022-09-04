using Contactos.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contactos.Data.Configurations
{
    public abstract class BaseConfiguration<Entity> where Entity: BaseEntity
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.HasKey(a => a.ID);
            builder.Property(a => a.ID).ValueGeneratedOnAdd();
        }
    }
}
