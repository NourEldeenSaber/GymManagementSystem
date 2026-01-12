using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagementDAL.Data.Configurations
{
    internal class HealthRecordConfiguration : IEntityTypeConfiguration<HealthRecord>
    {
        public void Configure(EntityTypeBuilder<HealthRecord> builder)
        {
            builder.ToTable("Members")
                .HasKey(X => X.Id); // NOT Needed [By Convention]

            builder.HasOne<Member>()
                .WithOne(X => X.HealthRecord)
                .HasForeignKey<HealthRecord>(X => X.Id);

            builder.Ignore(X=>X.CreatedAt);
            builder.Ignore(X=>X.UpdatedAt);
        }
    }
}
