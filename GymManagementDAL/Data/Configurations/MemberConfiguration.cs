using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementDAL.Data.Configurations
{
    internal class MemberConfiguration : GymUserConfiguration<Member> , IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.Property(x => x.CreatedAt)
                .HasColumnName("JoinDate")
                .HasDefaultValueSql("GETDATE()");

            base.Configure(builder); // to get the base configure [ GymUserConfiguration ]

            
        }
    }
}
