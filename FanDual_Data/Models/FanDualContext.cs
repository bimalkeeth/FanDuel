using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FanDual_Data.Models;

public partial class FanDualContext : DbContext
{
    public FanDualContext()
    {
    }

    public FanDualContext(DbContextOptions<FanDualContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DepthChart> DepthCharts { get; set; }

    public virtual DbSet<HeadPosition> HeadPositions { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Sport> Sports { get; set; }

    public virtual DbSet<SportsPlayersDepth> SportsPlayersDepths { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamSport> TeamSports { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=/Users/bimalkeeth/RiderProjects/FanDuel/FanDual.sqlite;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DepthChart>(entity =>
        {
            entity.ToTable("depth_charts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Sport).WithMany(p => p.DepthCharts)
                .HasForeignKey(d => d.SportId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Team).WithMany(p => p.DepthCharts)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<HeadPosition>(entity =>
        {
            entity.ToTable("head_positions");

            entity.HasIndex(e => e.HeadCode, "IX_head_positions_head_code").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HeadCode).HasColumnName("head_code");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.ToTable("players");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Number).HasColumnName("number");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.ToTable("positions");

            entity.HasIndex(e => e.Code, "IX_positions_code").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.HeadPositionId).HasColumnName("head_position_id");

            entity.HasOne(d => d.HeadPosition).WithMany(p => p.Positions)
                .HasForeignKey(d => d.HeadPositionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Sport>(entity =>
        {
            entity.ToTable("sports");

            entity.HasIndex(e => e.SportName, "IX_sports_sport_name").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SportName).HasColumnName("sport_name");
        });

        modelBuilder.Entity<SportsPlayersDepth>(entity =>
        {
            entity.ToTable("sports_players_depths");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DepthChartsId).HasColumnName("depth_charts_id");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.PositionDepth).HasColumnName("position_depth");
            entity.Property(e => e.PositionId).HasColumnName("position_id");

            entity.HasOne(d => d.DepthCharts).WithMany(p => p.SportsPlayersDepths)
                .HasForeignKey(d => d.DepthChartsId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Player).WithMany(p => p.SportsPlayersDepths)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Position).WithMany(p => p.SportsPlayersDepths)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.ToTable("teams");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<TeamSport>(entity =>
        {
            entity.ToTable("team_sports");

            entity.HasIndex(e => new { e.TeamId, e.SportId }, "IX_team_sports_team_id_sport_id").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SportId).HasColumnName("sport_id");
            entity.Property(e => e.TeamId).HasColumnName("team_id");

            entity.HasOne(d => d.Sport).WithMany(p => p.TeamSports)
                .HasForeignKey(d => d.SportId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Team).WithMany(p => p.TeamSports)
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
