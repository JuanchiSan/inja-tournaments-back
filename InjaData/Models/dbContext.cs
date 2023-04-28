﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InjaData.Models;

public partial class dbContext : DbContext
{
    public dbContext()
    {
    }

    public dbContext(DbContextOptions<dbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Challenge> Challenges { get; set; }

    public virtual DbSet<Challengedivision> Challengedivisions { get; set; }

    public virtual DbSet<Challengejuzmentcriterion> Challengejuzmentcriteria { get; set; }

    public virtual DbSet<Channelpaid> Channelpaids { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Competitiontype> Competitiontypes { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<Doctype> Doctypes { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Eventchallenge> Eventchallenges { get; set; }

    public virtual DbSet<Eventdivision> Eventdivisions { get; set; }

    public virtual DbSet<Inscription> Inscriptions { get; set; }

    public virtual DbSet<Inscriptionchallenge> Inscriptionchallenges { get; set; }

    public virtual DbSet<Judgmentcriterion> Judgmentcriteria { get; set; }

    public virtual DbSet<Ninjagroup> Ninjagroups { get; set; }

    public virtual DbSet<Ninjauser> Ninjausers { get; set; }

    public virtual DbSet<Persongroup> Persongroups { get; set; }

    public virtual DbSet<Point> Points { get; set; }

    public virtual DbSet<Usertype> Usertypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(Helper.CS);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Challenge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("challenge_pk");

            entity.ToTable("challenge");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("active");
            entity.Property(e => e.Comment)
                .HasMaxLength(200)
                .HasColumnName("comment");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasMany(d => d.Challengeiddependencies).WithMany(p => p.Challenges)
                .UsingEntity<Dictionary<string, object>>(
                    "Challengedependancy",
                    r => r.HasOne<Challenge>().WithMany()
                        .HasForeignKey("Challengeiddependency")
                        .HasConstraintName("fk_challengedependancy_challenge"),
                    l => l.HasOne<Challenge>().WithMany()
                        .HasForeignKey("Challengeid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_challengedependancy_challenge1"),
                    j =>
                    {
                        j.HasKey("Challengeid", "Challengeiddependency").HasName("challengedependancy_pk");
                        j.ToTable("challengedependancy");
                        j.IndexerProperty<int>("Challengeid").HasColumnName("challengeid");
                        j.IndexerProperty<int>("Challengeiddependency").HasColumnName("challengeiddependency");
                    });

            entity.HasMany(d => d.Challenges).WithMany(p => p.Challengeiddependencies)
                .UsingEntity<Dictionary<string, object>>(
                    "Challengedependancy",
                    r => r.HasOne<Challenge>().WithMany()
                        .HasForeignKey("Challengeid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_challengedependancy_challenge1"),
                    l => l.HasOne<Challenge>().WithMany()
                        .HasForeignKey("Challengeiddependency")
                        .HasConstraintName("fk_challengedependancy_challenge"),
                    j =>
                    {
                        j.HasKey("Challengeid", "Challengeiddependency").HasName("challengedependancy_pk");
                        j.ToTable("challengedependancy");
                        j.IndexerProperty<int>("Challengeid").HasColumnName("challengeid");
                        j.IndexerProperty<int>("Challengeiddependency").HasColumnName("challengeiddependency");
                    });
        });

        modelBuilder.Entity<Challengedivision>(entity =>
        {
            entity.HasKey(e => new { e.Challengeid, e.Divisionid, e.Eventid }).HasName("challengedivision_pk");

            entity.ToTable("challengedivision");

            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Eventid).HasColumnName("eventid");

            entity.HasOne(d => d.Challenge).WithMany(p => p.Challengedivisions)
                .HasForeignKey(d => d.Challengeid)
                .HasConstraintName("fk_challengedivision_challenge");

            entity.HasOne(d => d.Division).WithMany(p => p.Challengedivisions)
                .HasForeignKey(d => d.Divisionid)
                .HasConstraintName("fk_challengedivision_division");

            entity.HasOne(d => d.Event).WithMany(p => p.Challengedivisions)
                .HasForeignKey(d => d.Eventid)
                .HasConstraintName("fk_challengedivision_event");
        });

        modelBuilder.Entity<Challengejuzmentcriterion>(entity =>
        {
            entity.HasKey(e => new { e.Challengeid, e.Criteriaid, e.Divisionid }).HasName("challengejuzmentcriteria_pk");

            entity.ToTable("challengejuzmentcriteria");

            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Criteriaid).HasColumnName("criteriaid");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Hands).HasColumnName("hands");
            entity.Property(e => e.Maxscore)
                .HasPrecision(12, 2)
                .HasColumnName("maxscore");
            entity.Property(e => e.Scorebydivision)
                .HasColumnType("json")
                .HasColumnName("scorebydivision");
            entity.Property(e => e.Slotcant).HasColumnName("slotcant");
            entity.Property(e => e.Slotstep)
                .HasPrecision(4, 2)
                .HasColumnName("slotstep");

            entity.HasOne(d => d.Challenge).WithMany(p => p.Challengejuzmentcriteria)
                .HasForeignKey(d => d.Challengeid)
                .HasConstraintName("fk_challengejuzmentcriteria_challenge");

            entity.HasOne(d => d.Criteria).WithMany(p => p.Challengejuzmentcriteria)
                .HasForeignKey(d => d.Criteriaid)
                .HasConstraintName("fk_challengejuzmentcriteria_judgmentcriteria");

            entity.HasOne(d => d.Division).WithMany(p => p.Challengejuzmentcriteria)
                .HasForeignKey(d => d.Divisionid)
                .HasConstraintName("fk_challengejuzmentcriteria_division");
        });

        modelBuilder.Entity<Channelpaid>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("channelpaid_pk");

            entity.ToTable("channelpaid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");

            entity.HasOne(d => d.Country).WithMany(p => p.Channelpaids)
                .HasForeignKey(d => d.Countryid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_channelpaid_country");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("city_pk");

            entity.ToTable("city");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("active");
            entity.Property(e => e.Countryid).HasColumnName("countryid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.Countryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_city_country");
        });

        modelBuilder.Entity<Competitiontype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("competitiontype_pk");

            entity.ToTable("competitiontype");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .HasDefaultValueSql("1")
                .HasColumnName("active");
            entity.Property(e => e.Comment)
                .HasMaxLength(2048)
                .HasColumnName("comment");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("country_pk");

            entity.ToTable("country");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Division>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("division_pk");

            entity.ToTable("division");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("active");
            entity.Property(e => e.Competitiontypeid).HasColumnName("competitiontypeid");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");

            entity.HasOne(d => d.Competitiontype).WithMany(p => p.Divisions)
                .HasForeignKey(d => d.Competitiontypeid)
                .HasConstraintName("fk_division_competition");
        });

        modelBuilder.Entity<Doctype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("doctype_pk");

            entity.ToTable("doctype");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("event_pk");

            entity.ToTable("event");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("active");
            entity.Property(e => e.Comment)
                .HasMaxLength(2048)
                .HasColumnName("comment");
            entity.Property(e => e.Creatoruserid).HasColumnName("creatoruserid");
            entity.Property(e => e.Enddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("enddate");
            entity.Property(e => e.Endinscription)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("endinscription");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Startdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startdate");
            entity.Property(e => e.Startinscription)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startinscription");

            entity.HasOne(d => d.Creatoruser).WithMany(p => p.Events)
                .HasForeignKey(d => d.Creatoruserid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_event_ninjauser");
        });

        modelBuilder.Entity<Eventchallenge>(entity =>
        {
            entity.HasKey(e => new { e.Eventid, e.Challengeid }).HasName("eventchallenge_pk");

            entity.ToTable("eventchallenge");

            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Enddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("enddate");
            entity.Property(e => e.Startdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startdate");

            entity.HasOne(d => d.Challenge).WithMany(p => p.Eventchallenges)
                .HasForeignKey(d => d.Challengeid)
                .HasConstraintName("fk_challengeevent_challenge");

            entity.HasOne(d => d.Event).WithMany(p => p.Eventchallenges)
                .HasForeignKey(d => d.Eventid)
                .HasConstraintName("fk_challengeevent_event");
        });

        modelBuilder.Entity<Eventdivision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("eventdivision_pk");

            entity.ToTable("eventdivision");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.DivisionId).HasColumnName("division_id");

            entity.HasOne(d => d.Division).WithMany(p => p.Eventdivisions)
                .HasForeignKey(d => d.DivisionId)
                .HasConstraintName("fk_eventdivision_division");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Eventdivision)
                .HasForeignKey<Eventdivision>(d => d.Id)
                .HasConstraintName("fk_eventdivision_event");
        });

        modelBuilder.Entity<Inscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("inscription_pk");

            entity.ToTable("inscription");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Channelid).HasColumnName("channelid");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Paid).HasColumnName("paid");
            entity.Property(e => e.Paidamount)
                .HasColumnType("money")
                .HasColumnName("paidamount");
            entity.Property(e => e.Paiddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("paiddate");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Channel).WithMany(p => p.Inscriptions)
                .HasForeignKey(d => d.Channelid)
                .HasConstraintName("fk_inscription_channelpaid");

            entity.HasOne(d => d.Event).WithMany(p => p.Inscriptions)
                .HasForeignKey(d => d.Eventid)
                .HasConstraintName("fk_inscription_event");

            entity.HasOne(d => d.User).WithMany(p => p.Inscriptions)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("fk_inscription_user");
        });

        modelBuilder.Entity<Inscriptionchallenge>(entity =>
        {
            entity.HasKey(e => new { e.Userid, e.Challengeid, e.Divisionid, e.Eventid }).HasName("inscriptionchallenge_pk");

            entity.ToTable("inscriptionchallenge");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Inscriptiondate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("inscriptiondate");
            entity.Property(e => e.Wonfirstplace).HasColumnName("wonfirstplace");

            entity.HasOne(d => d.User).WithMany(p => p.Inscriptionchallenges)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("fk_inscriptionchallenge_user");

            entity.HasOne(d => d.Challengedivision).WithMany(p => p.Inscriptionchallenges)
                .HasForeignKey(d => new { d.Challengeid, d.Divisionid, d.Eventid })
                .HasConstraintName("fk_inscriptionchallenge_challengedivision");
        });

        modelBuilder.Entity<Judgmentcriterion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("judgmentcriteria_pk");

            entity.ToTable("judgmentcriteria");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Namees)
                .HasMaxLength(200)
                .HasColumnName("namees");
            entity.Property(e => e.Namefr)
                .HasMaxLength(200)
                .HasColumnName("namefr");
            entity.Property(e => e.Nameit)
                .HasMaxLength(200)
                .HasColumnName("nameit");
            entity.Property(e => e.Namepr)
                .HasMaxLength(200)
                .HasColumnName("namepr");
        });

        modelBuilder.Entity<Ninjagroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ninjagroup_pk");

            entity.ToTable("ninjagroup");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");

            entity.HasOne(d => d.Event).WithMany(p => p.Ninjagroups)
                .HasForeignKey(d => d.Eventid)
                .HasConstraintName("fk_ninjagroup_event");
        });

        modelBuilder.Entity<Ninjauser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ninjauser_pk");

            entity.ToTable("ninjauser");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("active");
            entity.Property(e => e.Cityid).HasColumnName("cityid");
            entity.Property(e => e.Docid).HasColumnName("docid");
            entity.Property(e => e.Docnnmber)
                .HasMaxLength(20)
                .HasColumnName("docnnmber");
            entity.Property(e => e.Firstname)
                .HasMaxLength(80)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(80)
                .HasColumnName("lastname");
            entity.Property(e => e.Mail)
                .HasMaxLength(100)
                .HasColumnName("mail");
            entity.Property(e => e.Number)
                .HasMaxLength(10)
                .HasColumnName("number");
            entity.Property(e => e.Pass)
                .HasMaxLength(100)
                .HasColumnName("pass");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Street)
                .HasMaxLength(100)
                .HasColumnName("street");
            entity.Property(e => e.Usertype)
                .HasMaxLength(20)
                .HasColumnName("usertype");

            entity.HasOne(d => d.City).WithMany(p => p.Ninjausers)
                .HasForeignKey(d => d.Cityid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_person_city");

            entity.HasOne(d => d.Doc).WithMany(p => p.Ninjausers)
                .HasForeignKey(d => d.Docid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_person_doctype");
        });

        modelBuilder.Entity<Persongroup>(entity =>
        {
            entity.HasKey(e => new { e.Userid, e.Groupid }).HasName("persongroup_pk");

            entity.ToTable("persongroup");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Groupid).HasColumnName("groupid");
            entity.Property(e => e.Added)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("added");
            entity.Property(e => e.Enabled).HasColumnName("enabled");

            entity.HasOne(d => d.Group).WithMany(p => p.Persongroups)
                .HasForeignKey(d => d.Groupid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_persongroup_group");

            entity.HasOne(d => d.User).WithMany(p => p.Persongroups)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_persongroup_user");
        });

        modelBuilder.Entity<Point>(entity =>
        {
            entity.HasKey(e => new { e.Userid, e.Judgeid, e.Challengeid, e.Criteriaid, e.Divisionid }).HasName("point_pk");

            entity.ToTable("point");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Judgeid).HasColumnName("judgeid");
            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Criteriaid).HasColumnName("criteriaid");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Comment)
                .HasMaxLength(200)
                .HasColumnName("comment");
            entity.Property(e => e.Slot1)
                .HasPrecision(5, 2)
                .HasColumnName("slot1");
            entity.Property(e => e.Slot10)
                .HasPrecision(5, 2)
                .HasColumnName("slot10");
            entity.Property(e => e.Slot2)
                .HasPrecision(5, 2)
                .HasColumnName("slot2");
            entity.Property(e => e.Slot3)
                .HasPrecision(5, 2)
                .HasColumnName("slot3");
            entity.Property(e => e.Slot4)
                .HasPrecision(5, 2)
                .HasColumnName("slot4");
            entity.Property(e => e.Slot5)
                .HasPrecision(5, 2)
                .HasColumnName("slot5");
            entity.Property(e => e.Slot6)
                .HasPrecision(5, 2)
                .HasColumnName("slot6");
            entity.Property(e => e.Slot7)
                .HasPrecision(5, 2)
                .HasColumnName("slot7");
            entity.Property(e => e.Slot8)
                .HasPrecision(5, 2)
                .HasColumnName("slot8");
            entity.Property(e => e.Slot9)
                .HasPrecision(5, 2)
                .HasColumnName("slot9");

            entity.HasOne(d => d.Judge).WithMany(p => p.PointJudges)
                .HasForeignKey(d => d.Judgeid)
                .HasConstraintName("fk_points_judge");

            entity.HasOne(d => d.User).WithMany(p => p.PointUsers)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("fk_points_user");

            entity.HasOne(d => d.Challengejuzmentcriterion).WithMany(p => p.Points)
                .HasForeignKey(d => new { d.Challengeid, d.Criteriaid, d.Divisionid })
                .HasConstraintName("fk_points_challengejuzmentcriteria");
        });

        modelBuilder.Entity<Usertype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usertype_pk");

            entity.ToTable("usertype");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("active");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");

            entity.HasMany(d => d.Users).WithMany(p => p.Types)
                .UsingEntity<Dictionary<string, object>>(
                    "Ninjauserusertype",
                    r => r.HasOne<Ninjauser>().WithMany()
                        .HasForeignKey("Userid")
                        .HasConstraintName("fk_persontype_user"),
                    l => l.HasOne<Usertype>().WithMany()
                        .HasForeignKey("Typeid")
                        .HasConstraintName("fk_personaltype_usertype"),
                    j =>
                    {
                        j.HasKey("Typeid", "Userid").HasName("ninjauserusertype_pk");
                        j.ToTable("ninjauserusertype");
                        j.IndexerProperty<int>("Typeid").HasColumnName("typeid");
                        j.IndexerProperty<int>("Userid").HasColumnName("userid");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
