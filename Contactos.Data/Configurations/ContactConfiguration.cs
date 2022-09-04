using Contactos.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contactos.Data.Configurations
{
    public class ContactConfiguration: BaseConfiguration<Contact>, IEntityTypeConfiguration<Contact>
    {
        new public void Configure(EntityTypeBuilder<Contact> builder)
        {
            base.Configure(builder);
            builder.ToTable("Contact");
        }
    }
}
