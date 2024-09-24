using GraphQl.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQl.Database.Configurations
{
    public class GroceryConfiguration : IEntityTypeConfiguration<Grocery>
    {
        public void Configure(EntityTypeBuilder<Grocery> builder)
        {

        }
    }
}

