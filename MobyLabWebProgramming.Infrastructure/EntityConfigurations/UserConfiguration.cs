﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

/// <summary>
/// This is the entity configuration for the User entity, generally the Entity Framework will figure out most of the configuration but,
/// for some specifics such as unique keys, indexes and foreign keys it is better to explicitly specify them.
/// Note that the EntityTypeBuilder implements a Fluent interface, meaning it is a highly declarative interface using method-chaining.
/// </summary>
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(e => e.Id) // This specifies which property is configured.
            .IsRequired();
        // Here it is specified if the property is required, meaning it cannot be null in the database.
        builder.HasKey(x => x.Id);
        // Here it is specified that the property Id is the primary key.
        builder.Property(e => e.Name)
            .HasMaxLength(255) // This specifies the maximum length for varchar type in the database.
            .IsRequired();
        
        builder.Property(e => e.Email)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.HasAlternateKey(e => e.Email); // Here it is specified that the property Email is a unique key.
        
        builder.Property(e => e.Password)
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(e => e.Role)
            .HasConversion(new EnumToStringConverter<UserRoleEnum>())
            .HasMaxLength(255)
            .IsRequired();
        
        builder.HasMany(u => u.Applications)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(u => u.Notifications)
            .WithOne(n => n.User)
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
    }
}
