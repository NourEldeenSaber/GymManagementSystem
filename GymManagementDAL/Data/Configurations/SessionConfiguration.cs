using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GymManagementDAL.Data.Configurations
{
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(Tb => {
                Tb.HasCheckConstraint("SessionCapacityCheck", "Capacity Between 1 and 25");
                Tb.HasCheckConstraint("SessionEndDateCheck", "EndDate > StartDate");
            });

            // 1 - M [Category - Session]
            builder.HasOne(x=>x.SessionCategory)
                .WithMany(x => x.Sessions)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
