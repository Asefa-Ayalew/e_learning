using ELearning.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ELearning.Infrastructure.Persistence.Configurations;

public sealed class RefreshTokenConfiguration
    : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");

        // ============================================================
        // PRIMARY KEY
        // ============================================================

        builder.HasKey(refreshToken => refreshToken.Id);

        builder.Property(refreshToken => refreshToken.Id)
            .HasColumnName("id");

        // ============================================================
        // TOKEN
        // ============================================================

        builder.Property(refreshToken => refreshToken.Token)
            .HasColumnName("token")
            .IsRequired()
            .HasMaxLength(500);

        builder.HasIndex(refreshToken => refreshToken.Token)
            .IsUnique();

        // ============================================================
        // USER
        // ============================================================

        builder.Property(refreshToken => refreshToken.UserId)
            .HasColumnName("user_id")
            .IsRequired();

        builder.HasOne(refreshToken => refreshToken.User)
            .WithMany(user => user.RefreshTokens)
            .HasForeignKey(refreshToken => refreshToken.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // ============================================================
        // DATES
        // ============================================================

        builder.Property(refreshToken => refreshToken.ExpiresAt)
            .HasColumnName("expires_at")
            .IsRequired();

        builder.Property(refreshToken => refreshToken.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(refreshToken => refreshToken.RevokedAt)
            .HasColumnName("revoked_at");

        builder.Property(refreshToken => refreshToken.ReplacedByToken)
            .HasColumnName("replaced_by_token")
            .HasMaxLength(500);
    }
}