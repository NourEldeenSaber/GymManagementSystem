using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementDAL.Data.Configurations
{
    internal class GymUserConfiguration<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(X => X.Name)
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Property(X => X.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100);

            // Add Constraines
            builder.ToTable(Tb => {
                Tb.HasCheckConstraint("GymUserValidEmailCheck", "Email Like '_%@_%._%' ");
                Tb.HasCheckConstraint("GymUserValidPhoneCheck", "Phone Like '01%' and phone not like '%[^0-9]%' ");
            });

            // Add Clusterd Index
            builder.HasIndex(X => X.Email).IsUnique();
            builder.HasIndex(X => X.Phone).IsUnique();

            builder.OwnsOne(X => X.Address, AddressBuilder =>
            {
                AddressBuilder.Property(X => X.Street)
                .HasColumnName("Street")
                .HasColumnType("varchar")
                .HasMaxLength(30);

                AddressBuilder.Property(X => X.City)
                .HasColumnName("City")
                .HasColumnType("varchar")
                .HasMaxLength(30);

                AddressBuilder.Property(X => X.BuildingNumber)
                .HasColumnName("BuildingNumber");
            });





        }
    }
}
