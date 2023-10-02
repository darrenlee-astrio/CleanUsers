using CleanUsers.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanUsers.Infrastructure.Users.Persistence;

public class UsersConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.Username)
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Email)
            .IsRequired();

        builder.Property(x => x.UserType)
            .IsRequired()
            .HasConversion(
                userType => userType.Value, // write
                value => UserType.FromValue(value)); // read

        builder.Property(x => x.DateJoined)
            .IsRequired();
    }
}
