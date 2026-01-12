using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementDAL.Data.Configurations
{
    internal class MemberSessionConfiguration : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
            builder.Ignore(x => x.Id);
            builder.HasKey(x => new { x.MemberId, x.SessionId });

            builder.Property(x => x.CreatedAt)
                .HasColumnName("BookingDate")
                .HasDefaultValueSql("GetDate()");


        }
    }
}
