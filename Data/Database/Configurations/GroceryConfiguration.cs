using System;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations
{
    public class GroceryConfiguration : IEntityTypeConfiguration<Grocery>
    {
        public void Configure(EntityTypeBuilder<Grocery> builder)
        {

        }
    }
}

