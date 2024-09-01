using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TaskWave.Domain.Entities.Groups;
using TaskWave.Infrastructure.Persistence.Abstractions.Configurations;
using TaskWave.Infrastructure.Persistence.Ulids;

namespace TaskWave.Infrastructure.Persistence.Configurations;

public class GroupConfiguration : EntityConfiguration<Group>
{
    public override void Configure(EntityTypeBuilder<Group> builder)
    {
        base.Configure(builder);

        builder.ToTable("Groups");

        builder.Property(g => g.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.OwnsMany(g => g.Members, member =>
            {
                member.ToTable("GroupMembers");
                member.WithOwner().HasForeignKey("GroupId");
                member.Property(m => m.UserId).IsRequired();
                member.Property(m => m.JoinedDate).IsRequired();
                member.HasKey("UserId");
                UlidConfig.ConfigureUlid(member, m => m.UserId);
                member.HasIndex(m => m.UserId).HasDatabaseName("IX_GroupMembers_UserId");
            });

        // Make the private _members field part of the model
        builder.Navigation(g => g.Members)
            .UsePropertyAccessMode(PropertyAccessMode.Field); // This maps the private field _members

        builder.HasIndex(g => g.Name).IsUnique().HasDatabaseName("IX_Groups_Name");
    }
}