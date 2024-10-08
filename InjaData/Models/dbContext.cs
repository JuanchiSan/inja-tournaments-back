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

    public virtual DbSet<Challengejuzmentcriterion> Challengejuzmentcriteria { get; set; }

    public virtual DbSet<Challengetype> Challengetypes { get; set; }

    public virtual DbSet<Channelpaid> Channelpaids { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Competitiontype> Competitiontypes { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Deduction> Deductions { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<Doctype> Doctypes { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventCup> EventCups { get; set; }

    public virtual DbSet<Eventchallenge> Eventchallenges { get; set; }

    public virtual DbSet<Eventchallengedivision> Eventchallengedivisions { get; set; }

    public virtual DbSet<Eventjudgechallengedivision> Eventjudgechallengedivisions { get; set; }

    public virtual DbSet<Injagroup> Injagroups { get; set; }

    public virtual DbSet<Injauser> Injausers { get; set; }

    public virtual DbSet<Injauserusertype> Injauserusertypes { get; set; }

    public virtual DbSet<Inscription> Inscriptions { get; set; }

    public virtual DbSet<Judgmentcriterion> Judgmentcriteria { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Persongroup> Persongroups { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Point> Points { get; set; }

    public virtual DbSet<Recoveremail> Recoveremails { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Teaminscription> Teaminscriptions { get; set; }

    public virtual DbSet<Userinscription> Userinscriptions { get; set; }

    public virtual DbSet<Usertype> Usertypes { get; set; }

    public virtual DbSet<VChallengejudgementcriterion> VChallengejudgementcriteria { get; set; }

    public virtual DbSet<VChallengetypeEventchallengeCriterion> VChallengetypeEventchallengeCriteria { get; set; }

    public virtual DbSet<VContenderInscriptionCountByDivision> VContenderInscriptionCountByDivisions { get; set; }

    public virtual DbSet<VContenderInscriptionsCount> VContenderInscriptionsCounts { get; set; }

    public virtual DbSet<VCriteriasJudge> VCriteriasJudges { get; set; }

    public virtual DbSet<VCriteriasJudgesPlana> VCriteriasJudgesPlanas { get; set; }

    public virtual DbSet<VCupNailArt> VCupNailArts { get; set; }

    public virtual DbSet<VCupNailArtBase> VCupNailArtBases { get; set; }

    public virtual DbSet<VCupNailArtParticipantsOk> VCupNailArtParticipantsOks { get; set; }

    public virtual DbSet<VCupNailArtSigned> VCupNailArtSigneds { get; set; }

    public virtual DbSet<VDeduction> VDeductions { get; set; }

    public virtual DbSet<VDivision> VDivisions { get; set; }

    public virtual DbSet<VEventchallengedivision> VEventchallengedivisions { get; set; }

    public virtual DbSet<VEventchallengedivisionPlana> VEventchallengedivisionPlanas { get; set; }

    public virtual DbSet<VEventjudgechallengedivisionPlana> VEventjudgechallengedivisionPlanas { get; set; }

    public virtual DbSet<VGrandChampion> VGrandChampions { get; set; }

    public virtual DbSet<VInjagroup> VInjagroups { get; set; }

    public virtual DbSet<VInjagroupPlana> VInjagroupPlanas { get; set; }

    public virtual DbSet<VInjagroupPointsPlana> VInjagroupPointsPlanas { get; set; }

    public virtual DbSet<VInjagroupResult> VInjagroupResults { get; set; }

    public virtual DbSet<VInjauser> VInjausers { get; set; }

    public virtual DbSet<VNailCupPoint> VNailCupPoints { get; set; }

    public virtual DbSet<VUserinscriptionPlana> VUserinscriptionPlanas { get; set; }

    public virtual DbSet<VUserpoint> VUserpoints { get; set; }

    public virtual DbSet<VUserpointGroup> VUserpointGroups { get; set; }

    public virtual DbSet<VUserpointGroupEvaluated> VUserpointGroupEvaluateds { get; set; }

    public virtual DbSet<VUserschallengecriterion> VUserschallengecriteria { get; set; }

    public virtual DbSet<VWinnerOfWinner> VWinnerOfWinners { get; set; }

    public virtual DbSet<VWinnersByChallengeDivision> VWinnersByChallengeDivisions { get; set; }

    public virtual DbSet<VmNailCupInscription> VmNailCupInscriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(Helper.CS);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Challengejuzmentcriterion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("challengejuzmentcriteria_pk");

            entity.ToTable("challengejuzmentcriteria");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Criteriaid).HasColumnName("criteriaid");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Hands).HasColumnName("hands");
            entity.Property(e => e.Maxscore)
                .HasPrecision(12, 2)
                .HasColumnName("maxscore");
            entity.Property(e => e.Rounds)
                .HasDefaultValueSql("1")
                .HasColumnName("rounds");
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
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_challengejuzmentcriteria_division");
        });

        modelBuilder.Entity<Challengetype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("challenge_pk");

            entity.ToTable("challengetype");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('challenge_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("active");
            entity.Property(e => e.Comment)
                .HasMaxLength(200)
                .HasColumnName("comment");
            entity.Property(e => e.Isforteam).HasColumnName("isforteam");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
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

        modelBuilder.Entity<Deduction>(entity =>
        {
            entity.HasKey(e => new { e.Suptypeid, e.Supuserid, e.Supeventid, e.Ceventid, e.Cuserid, e.Ctypeid, e.Cdivisionid, e.Ceventchallengeid, e.Deductionid }).HasName("deduction_pk");

            entity.ToTable("deduction");

            entity.Property(e => e.Suptypeid).HasColumnName("suptypeid");
            entity.Property(e => e.Supuserid).HasColumnName("supuserid");
            entity.Property(e => e.Supeventid).HasColumnName("supeventid");
            entity.Property(e => e.Ceventid).HasColumnName("ceventid");
            entity.Property(e => e.Cuserid).HasColumnName("cuserid");
            entity.Property(e => e.Ctypeid).HasColumnName("ctypeid");
            entity.Property(e => e.Cdivisionid).HasColumnName("cdivisionid");
            entity.Property(e => e.Ceventchallengeid).HasColumnName("ceventchallengeid");
            entity.Property(e => e.Deductionid).HasColumnName("deductionid");
            entity.Property(e => e.Comment)
                .HasMaxLength(200)
                .HasColumnName("comment");
            entity.Property(e => e.Creationdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creationdate");
            entity.Property(e => e.Score)
                .HasPrecision(5, 2)
                .HasColumnName("score");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");

            entity.HasOne(d => d.Sup).WithMany(p => p.Deductions)
                .HasForeignKey(d => new { d.Suptypeid, d.Supuserid, d.Supeventid })
                .HasConstraintName("fk_deduction_injauserjudge");

            entity.HasOne(d => d.C).WithMany(p => p.Deductions)
                .HasForeignKey(d => new { d.Ceventchallengeid, d.Cdivisionid, d.Ctypeid, d.Cuserid, d.Ceventid })
                .HasConstraintName("fk_deducion_inscription");
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
            entity.Property(e => e.PointPublished).HasColumnName("point_published");
            entity.Property(e => e.PointPublishedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("point_published_date");
            entity.Property(e => e.PointPublishedMessage)
                .HasMaxLength(2048)
                .HasDefaultValueSql("'Not Published'::character varying")
                .HasColumnName("point_published_message");
            entity.Property(e => e.PointPublishedUser)
                .HasMaxLength(50)
                .HasColumnName("point_published_user");
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

        modelBuilder.Entity<EventCup>(entity =>
        {
            entity.HasKey(e => new { e.EventId, e.Cup }).HasName("event_cups_pk");

            entity.ToTable("event_cups");

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Cup)
                .HasMaxLength(50)
                .HasColumnName("cup");

            entity.HasOne(d => d.Event).WithMany(p => p.EventCups)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("event_cups_event_id_fk");
        });

        modelBuilder.Entity<Eventchallenge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("eventchallenge_pk");

            entity.ToTable("eventchallenge");

            entity.HasIndex(e => new { e.Eventid, e.Challengeid, e.Startdate }, "eventchallenge_ak").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Enddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("enddate");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValueSql("'default'::character varying")
                .HasColumnName("name");
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

        modelBuilder.Entity<Eventchallengedivision>(entity =>
        {
            entity.HasKey(e => new { e.Eventchallengeid, e.Divisionid }).HasName("eventchallengedivision_pk");

            entity.ToTable("eventchallengedivision");

            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Minimumcontnders)
                .HasDefaultValueSql("4")
                .HasColumnName("minimumcontnders");

            entity.HasOne(d => d.Division).WithMany(p => p.Eventchallengedivisions)
                .HasForeignKey(d => d.Divisionid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eventchallengedivision_division_id_fk");

            entity.HasOne(d => d.Eventchallenge).WithMany(p => p.Eventchallengedivisions)
                .HasForeignKey(d => d.Eventchallengeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("eventchallengedivision_eventchallenge_id_fk");
        });

        modelBuilder.Entity<Eventjudgechallengedivision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_eventjudgechallengedivision");

            entity.ToTable("eventjudgechallengedivision");

            entity.HasIndex(e => new { e.Divisionid, e.Eventchallengeid, e.Challengejuzmentcriteria, e.Judgeid }, "idx_event_judge_challenge_division").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("active");
            entity.Property(e => e.Challengejuzmentcriteria).HasColumnName("challengejuzmentcriteria");
            entity.Property(e => e.Comment)
                .HasMaxLength(1024)
                .HasColumnName("comment");
            entity.Property(e => e.Creationdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creationdate");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Judgeid).HasColumnName("judgeid");
            entity.Property(e => e.Updateddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updateddate");

            entity.HasOne(d => d.ChallengejuzmentcriteriaNavigation).WithMany(p => p.Eventjudgechallengedivisions)
                .HasForeignKey(d => d.Challengejuzmentcriteria)
                .HasConstraintName("fk_ejcd_cjc");

            entity.HasOne(d => d.Judge).WithMany(p => p.Eventjudgechallengedivisions)
                .HasForeignKey(d => d.Judgeid)
                .HasConstraintName("fk_ejcd_user");

            entity.HasOne(d => d.Eventchallengedivision).WithMany(p => p.Eventjudgechallengedivisions)
                .HasForeignKey(d => new { d.Eventchallengeid, d.Divisionid })
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ejcd_ecd");
        });

        modelBuilder.Entity<Injagroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("injagroup_pk");

            entity.ToTable("injagroup");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('injagroup_id2_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Isstudent).HasColumnName("isstudent");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");

            entity.HasOne(d => d.Event).WithMany(p => p.Injagroups)
                .HasForeignKey(d => d.Eventid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("injagroup_event");
        });

        modelBuilder.Entity<Injauser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("injauser_pk");

            entity.ToTable("injauser");

            entity.HasIndex(e => e.Mail, "idx_injauser_email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("active");
            entity.Property(e => e.Adminusertype)
                .HasComment("Tipo de Usuario para la parte de Administración")
                .HasColumnName("adminusertype");
            entity.Property(e => e.Cityid).HasColumnName("cityid");
            entity.Property(e => e.Docid).HasColumnName("docid");
            entity.Property(e => e.Docnumber)
                .HasMaxLength(20)
                .HasColumnName("docnumber");
            entity.Property(e => e.Firstname)
                .HasMaxLength(80)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(80)
                .HasColumnName("lastname");
            entity.Property(e => e.Mail)
                .HasMaxLength(100)
                .HasColumnName("mail");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasComputedColumnSql("(((lastname)::text || ', '::text) || (firstname)::text)", true)
                .HasColumnName("name");
            entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .HasColumnName("nickname");
            entity.Property(e => e.Number)
                .HasMaxLength(10)
                .HasColumnName("number");
            entity.Property(e => e.Pass)
                .HasMaxLength(100)
                .HasColumnName("pass");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.PreferredLanguage)
                .HasMaxLength(120)
                .HasDefaultValueSql("'En'::character varying")
                .HasColumnName("preferred_language");
            entity.Property(e => e.Street)
                .HasMaxLength(100)
                .HasColumnName("street");
            entity.Property(e => e.Urlphoto)
                .HasMaxLength(200)
                .HasColumnName("urlphoto");

            entity.HasOne(d => d.City).WithMany(p => p.Injausers)
                .HasForeignKey(d => d.Cityid)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_person_city");

            entity.HasOne(d => d.Doc).WithMany(p => p.Injausers)
                .HasForeignKey(d => d.Docid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_person_doctype");
        });

        modelBuilder.Entity<Injauserusertype>(entity =>
        {
            entity.HasKey(e => new { e.Typeid, e.Userid, e.Eventid }).HasName("injauserusertype_pk");

            entity.ToTable("injauserusertype");

            entity.Property(e => e.Typeid).HasColumnName("typeid");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.UserNumber)
                .HasMaxLength(10)
                .HasColumnName("user_number");

            entity.HasOne(d => d.Event).WithMany(p => p.Injauserusertypes)
                .HasForeignKey(d => d.Eventid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_injausertype_event");

            entity.HasOne(d => d.Type).WithMany(p => p.Injauserusertypes)
                .HasForeignKey(d => d.Typeid)
                .HasConstraintName("fk_personaltype_usertype");

            entity.HasOne(d => d.User).WithMany(p => p.Injauserusertypes)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("fk_persontype_user");
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

        modelBuilder.Entity<Log>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("logs");

            entity.Property(e => e.Exception).HasColumnName("exception");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.LogEvent)
                .HasColumnType("jsonb")
                .HasColumnName("log_event");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.MessageTemplate).HasColumnName("message_template");
            entity.Property(e => e.Timestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<Persongroup>(entity =>
        {
            entity.HasKey(e => new { e.Userid, e.Groupid }).HasName("persongroup_pk");

            entity.ToTable("persongroup");

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Groupid).HasColumnName("groupid");
            entity.Property(e => e.Added)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("added");
            entity.Property(e => e.Enabled)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("enabled");

            entity.HasOne(d => d.Group).WithMany(p => p.Persongroups)
                .HasForeignKey(d => d.Groupid)
                .HasConstraintName("persongroup_injagroup_id_fk");

            entity.HasOne(d => d.User).WithMany(p => p.Persongroups)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_persongroup_user");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => new { e.Eventid, e.Challengeid, e.Divisionid, e.Contenderid, e.Photographerid, e.Created }).HasName("photo_pk");

            entity.ToTable("photo");

            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Photographerid).HasColumnName("photographerid");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.Filename)
                .HasMaxLength(100)
                .HasColumnName("filename");
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(512)
                .HasColumnName("photoUrl");
            entity.Property(e => e.StoredFileName)
                .HasMaxLength(250)
                .HasColumnName("storedFileName");
            entity.Property(e => e.Updated)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated");

            entity.HasOne(d => d.Challenge).WithMany(p => p.Photos)
                .HasForeignKey(d => d.Challengeid)
                .HasConstraintName("fk_photo_challenge");

            entity.HasOne(d => d.Contender).WithMany(p => p.PhotoContenders)
                .HasForeignKey(d => d.Contenderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_photo_contender");

            entity.HasOne(d => d.Event).WithMany(p => p.Photos)
                .HasForeignKey(d => d.Eventid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_photo_event");

            entity.HasOne(d => d.Photographer).WithMany(p => p.PhotoPhotographers)
                .HasForeignKey(d => d.Photographerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_photo_photographer");
        });

        modelBuilder.Entity<Point>(entity =>
        {
            entity.HasKey(e => new { e.Eventjudgechallengeid, e.Userid }).HasName("point_pk");

            entity.ToTable("point");

            entity.Property(e => e.Eventjudgechallengeid).HasColumnName("eventjudgechallengeid");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Comment)
                .HasMaxLength(200)
                .HasColumnName("comment");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creationDate");
            entity.Property(e => e.ModifiedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("modifiedDate");
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
            entity.Property(e => e.Totalpoints)
                .HasPrecision(8, 2)
                .HasComputedColumnSql("(((((((((COALESCE(slot1, (0)::numeric) + COALESCE(slot2, (0)::numeric)) + COALESCE(slot3, (0)::numeric)) + COALESCE(slot4, (0)::numeric)) + COALESCE(slot5, (0)::numeric)) + COALESCE(slot6, (0)::numeric)) + COALESCE(slot7, (0)::numeric)) + COALESCE(slot8, (0)::numeric)) + COALESCE(slot9, (0)::numeric)) + COALESCE(slot10, (0)::numeric))", true)
                .HasColumnName("totalpoints");

            entity.HasOne(d => d.Eventjudgechallenge).WithMany(p => p.Points)
                .HasForeignKey(d => d.Eventjudgechallengeid)
                .HasConstraintName("fk_Point_eventjudgechallengedivision");

            entity.HasOne(d => d.User).WithMany(p => p.Points)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("fk_points_user");
        });

        modelBuilder.Entity<Recoveremail>(entity =>
        {
            entity.HasKey(e => new { e.Email, e.CreationDate }).HasName("pk_recoveremail");

            entity.ToTable("recoveremail");

            entity.HasIndex(e => e.Email, "idx_recoverpass_email");

            entity.HasIndex(e => e.Token, "idx_token").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("email");
            entity.Property(e => e.CreationDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Token)
                .HasMaxLength(40)
                .HasColumnName("token");
            entity.Property(e => e.UsedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("used_date");
            entity.Property(e => e.ValidUntilDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("valid_until_date");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("id");

            entity.ToTable("team");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasOne(d => d.Event).WithMany(p => p.Teams)
                .HasForeignKey(d => d.Eventid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("team_event");

            entity.HasMany(d => d.Injausers).WithMany(p => p.Teams)
                .UsingEntity<Dictionary<string, object>>(
                    "Userteam",
                    r => r.HasOne<Injauser>().WithMany()
                        .HasForeignKey("Injauserid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("userteam_injauser"),
                    l => l.HasOne<Team>().WithMany()
                        .HasForeignKey("Teamid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("userteam_team"),
                    j =>
                    {
                        j.HasKey("Teamid", "Injauserid").HasName("userteam_pk");
                        j.ToTable("userteam");
                        j.IndexerProperty<int>("Teamid").HasColumnName("teamid");
                        j.IndexerProperty<int>("Injauserid").HasColumnName("injauserid");
                    });
        });

        modelBuilder.Entity<Teaminscription>(entity =>
        {
            entity.HasKey(e => new { e.Eventchallengeid, e.Divisionid, e.Teamid }).HasName("teaminscription_pk");

            entity.ToTable("teaminscription");

            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Teamid).HasColumnName("teamid");
            entity.Property(e => e.Inscriptiondate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("inscriptiondate");

            entity.HasOne(d => d.Team).WithMany(p => p.Teaminscriptions)
                .HasForeignKey(d => d.Teamid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("groupinscription_team");
        });

        modelBuilder.Entity<Userinscription>(entity =>
        {
            entity.HasKey(e => new { e.Eventchallengeid, e.Divisionid, e.Utypeid, e.Uuserid, e.Ueventid }).HasName("userinscription_pk");

            entity.ToTable("userinscription");

            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Utypeid).HasColumnName("utypeid");
            entity.Property(e => e.Uuserid).HasColumnName("uuserid");
            entity.Property(e => e.Ueventid).HasColumnName("ueventid");
            entity.Property(e => e.Inscriptiondate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("inscriptiondate");
            entity.Property(e => e.Wonfirstplace).HasColumnName("wonfirstplace");

            entity.HasOne(d => d.Eventchallengedivision).WithMany(p => p.Userinscriptions)
                .HasForeignKey(d => new { d.Eventchallengeid, d.Divisionid })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_userincription_eventchallengedivision");

            entity.HasOne(d => d.U).WithMany(p => p.Userinscriptions)
                .HasForeignKey(d => new { d.Utypeid, d.Uuserid, d.Ueventid })
                .HasConstraintName("fk_userinscription_injausertype");
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
        });

        modelBuilder.Entity<VChallengejudgementcriterion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_challengejudgementcriteria");

            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Challengetypename)
                .HasMaxLength(50)
                .HasColumnName("challengetypename");
            entity.Property(e => e.Criteriaid).HasColumnName("criteriaid");
            entity.Property(e => e.Criterianame)
                .HasMaxLength(200)
                .HasColumnName("criterianame");
            entity.Property(e => e.Criterianamees)
                .HasMaxLength(200)
                .HasColumnName("criterianamees");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Hands).HasColumnName("hands");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Maxscore)
                .HasPrecision(12, 2)
                .HasColumnName("maxscore");
            entity.Property(e => e.Rounds).HasColumnName("rounds");
            entity.Property(e => e.Slotcant).HasColumnName("slotcant");
            entity.Property(e => e.Slotstep)
                .HasPrecision(4, 2)
                .HasColumnName("slotstep");
        });

        modelBuilder.Entity<VChallengetypeEventchallengeCriterion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_challengetype_eventchallenge_criteria");

            entity.Property(e => e.Challengejudgementid).HasColumnName("challengejudgementid");
            entity.Property(e => e.Challengetypeid).HasColumnName("challengetypeid");
            entity.Property(e => e.Challengetypename)
                .HasMaxLength(50)
                .HasColumnName("challengetypename");
            entity.Property(e => e.Criteriaid).HasColumnName("criteriaid");
            entity.Property(e => e.Criterianame)
                .HasMaxLength(200)
                .HasColumnName("criterianame");
            entity.Property(e => e.Criterianamees)
                .HasMaxLength(200)
                .HasColumnName("criterianamees");
            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Eventchallengename)
                .HasMaxLength(100)
                .HasColumnName("eventchallengename");
            entity.Property(e => e.Hands).HasColumnName("hands");
            entity.Property(e => e.Maxscore)
                .HasPrecision(12, 2)
                .HasColumnName("maxscore");
            entity.Property(e => e.Rounds).HasColumnName("rounds");
            entity.Property(e => e.Slotcant).HasColumnName("slotcant");
            entity.Property(e => e.Slotstep)
                .HasPrecision(4, 2)
                .HasColumnName("slotstep");
        });

        modelBuilder.Entity<VContenderInscriptionCountByDivision>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_contender_inscription_count_by_division");

            entity.Property(e => e.Cant).HasColumnName("cant");
            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
        });

        modelBuilder.Entity<VContenderInscriptionsCount>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_contender_inscriptions_count");

            entity.Property(e => e.ContenderInscriptionWithPoints).HasColumnName("contender_inscription_with_points");
            entity.Property(e => e.ContenderInscriptions).HasColumnName("contender_inscriptions");
            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.TotalChallenges).HasColumnName("total_challenges");
        });

        modelBuilder.Entity<VCriteriasJudge>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_criterias_judges");

            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Criteriaid).HasColumnName("criteriaid");
            entity.Property(e => e.Criterianame)
                .HasMaxLength(200)
                .HasColumnName("criterianame");
            entity.Property(e => e.Divisionsjudges).HasColumnName("divisionsjudges");
            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Eventchallengename)
                .HasMaxLength(100)
                .HasColumnName("eventchallengename");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Rounds).HasColumnName("rounds");
        });

        modelBuilder.Entity<VCriteriasJudgesPlana>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_criterias_judges_plana");

            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Challengejudgementcriteriaid).HasColumnName("challengejudgementcriteriaid");
            entity.Property(e => e.Competitiontypeid).HasColumnName("competitiontypeid");
            entity.Property(e => e.Competitiontypename)
                .HasMaxLength(50)
                .HasColumnName("competitiontypename");
            entity.Property(e => e.Criteriaid).HasColumnName("criteriaid");
            entity.Property(e => e.Criterianame)
                .HasMaxLength(200)
                .HasColumnName("criterianame");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Eventchallengename)
                .HasMaxLength(100)
                .HasColumnName("eventchallengename");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Judgename)
                .HasMaxLength(200)
                .HasColumnName("judgename");
            entity.Property(e => e.Rounds).HasColumnName("rounds");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<VCupNailArt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_cup_nail_art");

            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Contendername)
                .HasMaxLength(200)
                .HasColumnName("contendername");
            entity.Property(e => e.Contendernumber)
                .HasMaxLength(10)
                .HasColumnName("contendernumber");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.FinalPoint).HasColumnName("final_point");
            entity.Property(e => e.Rank).HasColumnName("rank");
        });

        modelBuilder.Entity<VCupNailArtBase>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_cup_nail_art_base");

            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Contendername)
                .HasMaxLength(200)
                .HasColumnName("contendername");
            entity.Property(e => e.Contendernumber)
                .HasMaxLength(10)
                .HasColumnName("contendernumber");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Eventchallengename)
                .HasMaxLength(100)
                .HasColumnName("eventchallengename");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.FinalPoint).HasColumnName("final_point");
            entity.Property(e => e.Rank).HasColumnName("rank");
            entity.Property(e => e.Rown).HasColumnName("rown");
        });

        modelBuilder.Entity<VCupNailArtParticipantsOk>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_cup_nail_art_participants_ok");

            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
        });

        modelBuilder.Entity<VCupNailArtSigned>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_cup_nail_art_signed");

            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Contendername)
                .HasMaxLength(200)
                .HasColumnName("contendername");
            entity.Property(e => e.Contendernumber)
                .HasMaxLength(10)
                .HasColumnName("contendernumber");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
        });

        modelBuilder.Entity<VDeduction>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_deductions");

            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Comment)
                .HasMaxLength(200)
                .HasColumnName("comment");
            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Contendername)
                .HasMaxLength(200)
                .HasColumnName("contendername");
            entity.Property(e => e.Contendernumber)
                .HasMaxLength(10)
                .HasColumnName("contendernumber");
            entity.Property(e => e.Creationdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creationdate");
            entity.Property(e => e.Deductionnumber).HasColumnName("deductionnumber");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eventchallengename)
                .HasMaxLength(100)
                .HasColumnName("eventchallengename");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Judgeid).HasColumnName("judgeid");
            entity.Property(e => e.Judgename)
                .HasMaxLength(200)
                .HasColumnName("judgename");
            entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .HasColumnName("nickname");
            entity.Property(e => e.Score)
                .HasPrecision(5, 2)
                .HasColumnName("score");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");
        });

        modelBuilder.Entity<VDivision>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_divisions");

            entity.Property(e => e.Competitiontypeactive).HasColumnName("competitiontypeactive");
            entity.Property(e => e.Competitiontypecomment)
                .HasMaxLength(2048)
                .HasColumnName("competitiontypecomment");
            entity.Property(e => e.Competitiontypeid).HasColumnName("competitiontypeid");
            entity.Property(e => e.Competitiontypename)
                .HasMaxLength(50)
                .HasColumnName("competitiontypename");
            entity.Property(e => e.Divisionactive).HasColumnName("divisionactive");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
        });

        modelBuilder.Entity<VEventchallengedivision>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_eventchallengedivision");

            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Challengename)
                .HasMaxLength(50)
                .HasColumnName("challengename");
            entity.Property(e => e.Competitiontypeid).HasColumnName("competitiontypeid");
            entity.Property(e => e.Competitiontypename)
                .HasMaxLength(50)
                .HasColumnName("competitiontypename");
            entity.Property(e => e.Divisionnames).HasColumnName("divisionnames");
            entity.Property(e => e.Eventchallengeenddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("eventchallengeenddate");
            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Eventchallengename)
                .HasMaxLength(100)
                .HasColumnName("eventchallengename");
            entity.Property(e => e.Eventchallengestartdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("eventchallengestartdate");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Eventname)
                .HasMaxLength(50)
                .HasColumnName("eventname");
        });

        modelBuilder.Entity<VEventchallengedivisionPlana>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_eventchallengedivision_plana");

            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Challengename)
                .HasMaxLength(50)
                .HasColumnName("challengename");
            entity.Property(e => e.Competitiontypeid).HasColumnName("competitiontypeid");
            entity.Property(e => e.Competitiontypename)
                .HasMaxLength(50)
                .HasColumnName("competitiontypename");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eventchallengeenddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("eventchallengeenddate");
            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Eventchallengename)
                .HasMaxLength(100)
                .HasColumnName("eventchallengename");
            entity.Property(e => e.Eventchallengestartdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("eventchallengestartdate");
            entity.Property(e => e.Eventenddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("eventenddate");
            entity.Property(e => e.Eventendincriptiondate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("eventendincriptiondate");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Eventname)
                .HasMaxLength(50)
                .HasColumnName("eventname");
            entity.Property(e => e.Eventstartdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("eventstartdate");
            entity.Property(e => e.Eventstartincriptiondate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("eventstartincriptiondate");
        });

        modelBuilder.Entity<VEventjudgechallengedivisionPlana>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_eventjudgechallengedivision_plana");

            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Challengename)
                .HasMaxLength(50)
                .HasColumnName("challengename");
            entity.Property(e => e.Competitiontypeid).HasColumnName("competitiontypeid");
            entity.Property(e => e.Competitiontypename)
                .HasMaxLength(50)
                .HasColumnName("competitiontypename");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eventchallengeenddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("eventchallengeenddate");
            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Eventchallengename)
                .HasMaxLength(100)
                .HasColumnName("eventchallengename");
            entity.Property(e => e.Eventchallengestartdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("eventchallengestartdate");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Eventjudgechallengedivisionid).HasColumnName("eventjudgechallengedivisionid");
            entity.Property(e => e.Eventname)
                .HasMaxLength(50)
                .HasColumnName("eventname");
            entity.Property(e => e.Judgeid).HasColumnName("judgeid");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
        });

        modelBuilder.Entity<VGrandChampion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_grand_champion");

            entity.Property(e => e.Competitiontypeid).HasColumnName("competitiontypeid");
            entity.Property(e => e.Competitiontypename)
                .HasMaxLength(50)
                .HasColumnName("competitiontypename");
            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Contendername)
                .HasMaxLength(200)
                .HasColumnName("contendername");
            entity.Property(e => e.Contendernumber)
                .HasMaxLength(10)
                .HasColumnName("contendernumber");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Finalpoints).HasColumnName("finalpoints");
            entity.Property(e => e.Rank).HasColumnName("rank");
        });

        modelBuilder.Entity<VInjagroup>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_injagroup");

            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Groupid).HasColumnName("groupid");
            entity.Property(e => e.Groupname)
                .HasMaxLength(200)
                .HasColumnName("groupname");
            entity.Property(e => e.Participants).HasColumnName("participants");
        });

        modelBuilder.Entity<VInjagroupPlana>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_injagroup_plana");

            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Firstname)
                .HasMaxLength(80)
                .HasColumnName("firstname");
            entity.Property(e => e.Groupid).HasColumnName("groupid");
            entity.Property(e => e.Groupname)
                .HasMaxLength(200)
                .HasColumnName("groupname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(80)
                .HasColumnName("lastname");
            entity.Property(e => e.UserNumber)
                .HasMaxLength(10)
                .HasColumnName("user_number");
        });

        modelBuilder.Entity<VInjagroupPointsPlana>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_injagroup_points_plana");

            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Contendername)
                .HasMaxLength(200)
                .HasColumnName("contendername");
            entity.Property(e => e.Contendernumber)
                .HasMaxLength(10)
                .HasColumnName("contendernumber");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Eventchallengename)
                .HasMaxLength(100)
                .HasColumnName("eventchallengename");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Eventname)
                .HasMaxLength(50)
                .HasColumnName("eventname");
            entity.Property(e => e.FinalPoint).HasColumnName("final_point");
            entity.Property(e => e.GroupPoints).HasColumnName("group_points");
            entity.Property(e => e.GroupPosition).HasColumnName("group_position");
            entity.Property(e => e.Groupid).HasColumnName("groupid");
            entity.Property(e => e.Groupname)
                .HasMaxLength(200)
                .HasColumnName("groupname");
        });

        modelBuilder.Entity<VInjagroupResult>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_injagroup_result");

            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Eventname)
                .HasMaxLength(50)
                .HasColumnName("eventname");
            entity.Property(e => e.FinalPoint).HasColumnName("final_point");
            entity.Property(e => e.Groupid).HasColumnName("groupid");
            entity.Property(e => e.Groupname)
                .HasMaxLength(200)
                .HasColumnName("groupname");
        });

        modelBuilder.Entity<VInjauser>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_injauser");

            entity.Property(e => e.Cityid).HasColumnName("cityid");
            entity.Property(e => e.Docid).HasColumnName("docid");
            entity.Property(e => e.Docnumber)
                .HasMaxLength(20)
                .HasColumnName("docnumber");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Eventname)
                .HasMaxLength(50)
                .HasColumnName("eventname");
            entity.Property(e => e.Firstname)
                .HasMaxLength(80)
                .HasColumnName("firstname");
            entity.Property(e => e.Injauserid).HasColumnName("injauserid");
            entity.Property(e => e.Injausername)
                .HasMaxLength(200)
                .HasColumnName("injausername");
            entity.Property(e => e.Language)
                .HasMaxLength(120)
                .HasColumnName("language");
            entity.Property(e => e.Lastname)
                .HasMaxLength(80)
                .HasColumnName("lastname");
            entity.Property(e => e.Mail)
                .HasMaxLength(100)
                .HasColumnName("mail");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Typename)
                .HasMaxLength(50)
                .HasColumnName("typename");
            entity.Property(e => e.UserNumber)
                .HasMaxLength(10)
                .HasColumnName("user_number");
            entity.Property(e => e.Usertypeid).HasColumnName("usertypeid");
        });

        modelBuilder.Entity<VNailCupPoint>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_nail_cup_points");

            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Contendername)
                .HasMaxLength(200)
                .HasColumnName("contendername");
            entity.Property(e => e.Contendernumber)
                .HasMaxLength(10)
                .HasColumnName("contendernumber");
            entity.Property(e => e.Criteriaid).HasColumnName("criteriaid");
            entity.Property(e => e.Criterianame)
                .HasMaxLength(200)
                .HasColumnName("criterianame");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Eventchallengename)
                .HasMaxLength(100)
                .HasColumnName("eventchallengename");
            entity.Property(e => e.Totalpoints)
                .HasPrecision(8, 2)
                .HasColumnName("totalpoints");
        });

        modelBuilder.Entity<VUserinscriptionPlana>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_userinscription_plana");

            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Competitiontypeid).HasColumnName("competitiontypeid");
            entity.Property(e => e.Competitiontypename)
                .HasMaxLength(50)
                .HasColumnName("competitiontypename");
            entity.Property(e => e.Contendername)
                .HasMaxLength(200)
                .HasColumnName("contendername");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Eventchallengename)
                .HasMaxLength(100)
                .HasColumnName("eventchallengename");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Firstname)
                .HasMaxLength(80)
                .HasColumnName("firstname");
            entity.Property(e => e.Inscriptiondate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("inscriptiondate");
            entity.Property(e => e.Lastname)
                .HasMaxLength(80)
                .HasColumnName("lastname");
            entity.Property(e => e.Mail)
                .HasMaxLength(100)
                .HasColumnName("mail");
            entity.Property(e => e.Nickname)
                .HasMaxLength(50)
                .HasColumnName("nickname");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Usernumber)
                .HasMaxLength(10)
                .HasColumnName("usernumber");
            entity.Property(e => e.Usertypeid).HasColumnName("usertypeid");
            entity.Property(e => e.Usertypename)
                .HasMaxLength(50)
                .HasColumnName("usertypename");
        });

        modelBuilder.Entity<VUserpoint>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_userpoints");

            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Comment)
                .HasMaxLength(200)
                .HasColumnName("comment");
            entity.Property(e => e.Competitiontypeid).HasColumnName("competitiontypeid");
            entity.Property(e => e.Competitiontypename)
                .HasMaxLength(50)
                .HasColumnName("competitiontypename");
            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Contendername)
                .HasMaxLength(200)
                .HasColumnName("contendername");
            entity.Property(e => e.Contendernumber)
                .HasMaxLength(10)
                .HasColumnName("contendernumber");
            entity.Property(e => e.Criteriaid).HasColumnName("criteriaid");
            entity.Property(e => e.Criterianame)
                .HasMaxLength(200)
                .HasColumnName("criterianame");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Eventchallengename)
                .HasMaxLength(100)
                .HasColumnName("eventchallengename");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Eventjudgechallengedivisionid).HasColumnName("eventjudgechallengedivisionid");
            entity.Property(e => e.Eventname)
                .HasMaxLength(50)
                .HasColumnName("eventname");
            entity.Property(e => e.Hands).HasColumnName("hands");
            entity.Property(e => e.Judgeid).HasColumnName("judgeid");
            entity.Property(e => e.Judgename)
                .HasMaxLength(200)
                .HasColumnName("judgename");
            entity.Property(e => e.Maxscore)
                .HasPrecision(12, 2)
                .HasColumnName("maxscore");
            entity.Property(e => e.PointPublished).HasColumnName("point_published");
            entity.Property(e => e.PointPublishedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("point_published_date");
            entity.Property(e => e.PointPublishedMessage)
                .HasMaxLength(2048)
                .HasColumnName("point_published_message");
            entity.Property(e => e.PointPublishedUser)
                .HasMaxLength(50)
                .HasColumnName("point_published_user");
            entity.Property(e => e.Rounds).HasColumnName("rounds");
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
            entity.Property(e => e.Slotcant).HasColumnName("slotcant");
            entity.Property(e => e.Slotstep)
                .HasPrecision(4, 2)
                .HasColumnName("slotstep");
            entity.Property(e => e.Totalpoints)
                .HasPrecision(8, 2)
                .HasColumnName("totalpoints");
            entity.Property(e => e.VupKey).HasColumnName("vup_key");
        });

        modelBuilder.Entity<VUserpointGroup>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_userpoint_group");

            entity.Property(e => e.CantPhotos).HasColumnName("cant_photos");
            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Competitiontypeid).HasColumnName("competitiontypeid");
            entity.Property(e => e.Competitiontypename)
                .HasMaxLength(50)
                .HasColumnName("competitiontypename");
            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Contendername)
                .HasMaxLength(200)
                .HasColumnName("contendername");
            entity.Property(e => e.Contendernumber)
                .HasMaxLength(10)
                .HasColumnName("contendernumber");
            entity.Property(e => e.Criterio100).HasColumnName("criterio100");
            entity.Property(e => e.Criterio400).HasColumnName("criterio400");
            entity.Property(e => e.Criterio450).HasColumnName("criterio450");
            entity.Property(e => e.Criterio500).HasColumnName("criterio500");
            entity.Property(e => e.Criterio550).HasColumnName("criterio550");
            entity.Property(e => e.Criterio600).HasColumnName("criterio600");
            entity.Property(e => e.Criterio650).HasColumnName("criterio650");
            entity.Property(e => e.Criterio700).HasColumnName("criterio700");
            entity.Property(e => e.Criterio750).HasColumnName("criterio750");
            entity.Property(e => e.Criterio800).HasColumnName("criterio800");
            entity.Property(e => e.Criterio850).HasColumnName("criterio850");
            entity.Property(e => e.Criterio900).HasColumnName("criterio900");
            entity.Property(e => e.Criterio950).HasColumnName("criterio950");
            entity.Property(e => e.CriterioDificultad).HasColumnName("criterio_dificultad");
            entity.Property(e => e.Deductions).HasColumnName("deductions");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eval).HasColumnName("eval");
            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Eventchallengename)
                .HasMaxLength(100)
                .HasColumnName("eventchallengename");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Eventname)
                .HasMaxLength(50)
                .HasColumnName("eventname");
            entity.Property(e => e.FinalPoint).HasColumnName("final_point");
            entity.Property(e => e.Maxscore).HasColumnName("maxscore");
            entity.Property(e => e.NotEval).HasColumnName("not_eval");
            entity.Property(e => e.Totalpoints).HasColumnName("totalpoints");
        });

        modelBuilder.Entity<VUserpointGroupEvaluated>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_userpoint_group_evaluated");

            entity.Property(e => e.CantPhotos).HasColumnName("cant_photos");
            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Competitiontypeid).HasColumnName("competitiontypeid");
            entity.Property(e => e.Competitiontypename)
                .HasMaxLength(50)
                .HasColumnName("competitiontypename");
            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Contendername)
                .HasMaxLength(200)
                .HasColumnName("contendername");
            entity.Property(e => e.Contendernumber)
                .HasMaxLength(10)
                .HasColumnName("contendernumber");
            entity.Property(e => e.Criterio100).HasColumnName("criterio100");
            entity.Property(e => e.Criterio400).HasColumnName("criterio400");
            entity.Property(e => e.Criterio450).HasColumnName("criterio450");
            entity.Property(e => e.Criterio500).HasColumnName("criterio500");
            entity.Property(e => e.Criterio550).HasColumnName("criterio550");
            entity.Property(e => e.Criterio600).HasColumnName("criterio600");
            entity.Property(e => e.Criterio650).HasColumnName("criterio650");
            entity.Property(e => e.Criterio700).HasColumnName("criterio700");
            entity.Property(e => e.Criterio750).HasColumnName("criterio750");
            entity.Property(e => e.Criterio800).HasColumnName("criterio800");
            entity.Property(e => e.Criterio850).HasColumnName("criterio850");
            entity.Property(e => e.Criterio900).HasColumnName("criterio900");
            entity.Property(e => e.Criterio950).HasColumnName("criterio950");
            entity.Property(e => e.CriterioDificultad).HasColumnName("criterio_dificultad");
            entity.Property(e => e.Deductions).HasColumnName("deductions");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eval).HasColumnName("eval");
            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Eventchallengename)
                .HasMaxLength(100)
                .HasColumnName("eventchallengename");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Eventname)
                .HasMaxLength(50)
                .HasColumnName("eventname");
            entity.Property(e => e.FinalPoint).HasColumnName("final_point");
            entity.Property(e => e.Maxscore).HasColumnName("maxscore");
            entity.Property(e => e.NotEval).HasColumnName("not_eval");
            entity.Property(e => e.Totalpoints).HasColumnName("totalpoints");
        });

        modelBuilder.Entity<VUserschallengecriterion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_userschallengecriteria");

            entity.Property(e => e.ChallengeEnddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("challenge_enddate");
            entity.Property(e => e.ChallengeMinimumcontenders).HasColumnName("challenge_minimumcontenders");
            entity.Property(e => e.ChallengeName)
                .HasMaxLength(100)
                .HasColumnName("challenge_name");
            entity.Property(e => e.ChallengeStardate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("challenge_stardate");
            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.ContenderFirstname)
                .HasMaxLength(80)
                .HasColumnName("contender_firstname");
            entity.Property(e => e.ContenderLastname)
                .HasMaxLength(80)
                .HasColumnName("contender_lastname");
            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Criteriaid).HasColumnName("criteriaid");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Ecddivisionname)
                .HasMaxLength(200)
                .HasColumnName("ecddivisionname");
            entity.Property(e => e.Ecdivisionid).HasColumnName("ecdivisionid");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.EventjudgechallengedivisionComment)
                .HasMaxLength(1024)
                .HasColumnName("eventjudgechallengedivision_comment");
            entity.Property(e => e.EventjudgechallengedivisionCreationdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("eventjudgechallengedivision_creationdate");
            entity.Property(e => e.EventjudgechallengedivisionUpdatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("eventjudgechallengedivision_updatedate");
            entity.Property(e => e.Eventjudgechallengedivisionid).HasColumnName("eventjudgechallengedivisionid");
            entity.Property(e => e.Hands).HasColumnName("hands");
            entity.Property(e => e.JudgeFirstname)
                .HasMaxLength(80)
                .HasColumnName("judge_firstname");
            entity.Property(e => e.JudgeLastname)
                .HasMaxLength(80)
                .HasColumnName("judge_lastname");
            entity.Property(e => e.Judgeid).HasColumnName("judgeid");
            entity.Property(e => e.Judgementcriterianame)
                .HasMaxLength(200)
                .HasColumnName("judgementcriterianame");
            entity.Property(e => e.Maxscore)
                .HasPrecision(12, 2)
                .HasColumnName("maxscore");
            entity.Property(e => e.Rounds).HasColumnName("rounds");
            entity.Property(e => e.Scorebydivision)
                .HasColumnType("json")
                .HasColumnName("scorebydivision");
            entity.Property(e => e.Slotcant).HasColumnName("slotcant");
            entity.Property(e => e.Slotstep)
                .HasPrecision(4, 2)
                .HasColumnName("slotstep");
            entity.Property(e => e.Ueventid).HasColumnName("ueventid");
            entity.Property(e => e.Utypeid).HasColumnName("utypeid");
        });

        modelBuilder.Entity<VWinnerOfWinner>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_winner_of_winners");

            entity.Property(e => e.Competitiontypeid).HasColumnName("competitiontypeid");
            entity.Property(e => e.Competitiontypename)
                .HasMaxLength(50)
                .HasColumnName("competitiontypename");
            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Contendername)
                .HasMaxLength(200)
                .HasColumnName("contendername");
            entity.Property(e => e.Contendernumber)
                .HasMaxLength(10)
                .HasColumnName("contendernumber");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Finalpoints).HasColumnName("finalpoints");
            entity.Property(e => e.Rank).HasColumnName("rank");
        });

        modelBuilder.Entity<VWinnersByChallengeDivision>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("v_winners_by_challenge_division");

            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Contendername)
                .HasMaxLength(200)
                .HasColumnName("contendername");
            entity.Property(e => e.Contendernumber)
                .HasMaxLength(10)
                .HasColumnName("contendernumber");
            entity.Property(e => e.Deductions).HasColumnName("deductions");
            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Divisionname)
                .HasMaxLength(200)
                .HasColumnName("divisionname");
            entity.Property(e => e.Eventchallengeid).HasColumnName("eventchallengeid");
            entity.Property(e => e.Eventchallengename)
                .HasMaxLength(100)
                .HasColumnName("eventchallengename");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.FinalPoint).HasColumnName("final_point");
            entity.Property(e => e.Rank).HasColumnName("rank");
            entity.Property(e => e.Totalpoints).HasColumnName("totalpoints");
        });

        modelBuilder.Entity<VmNailCupInscription>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vm_nail_cup_inscriptions");

            entity.Property(e => e.Divisionid).HasColumnName("divisionid");
            entity.Property(e => e.Injauserid).HasColumnName("injauserid");
            entity.Property(e => e.Injausername)
                .HasMaxLength(200)
                .HasColumnName("injausername");
            entity.Property(e => e.UserNumber)
                .HasMaxLength(10)
                .HasColumnName("user_number");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
