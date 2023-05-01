using System;
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

    public virtual DbSet<Challengejuzmentcriterion> Challengejuzmentcriteria { get; set; }

    public virtual DbSet<Channelpaid> Channelpaids { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Competitiontype> Competitiontypes { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<Doctype> Doctypes { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Eventchallenge> Eventchallenges { get; set; }

    public virtual DbSet<Eventchallengedivision> Eventchallengedivisions { get; set; }

    public virtual DbSet<Injagroup> Injagroups { get; set; }

    public virtual DbSet<Injauser> Injausers { get; set; }

    public virtual DbSet<Injauserusertype> Injauserusertypes { get; set; }

    public virtual DbSet<Inscription> Inscriptions { get; set; }

    public virtual DbSet<Judgmentcriterion> Judgmentcriteria { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Persongroup> Persongroups { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Point> Points { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Teaminscription> Teaminscriptions { get; set; }

    public virtual DbSet<Userinscription> Userinscriptions { get; set; }

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
            entity.Property(e => e.Isforteam).HasColumnName("isforteam");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
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

            entity.HasMany(d => d.Divisions).WithMany(p => p.Events)
                .UsingEntity<Dictionary<string, object>>(
                    "Eventdivision",
                    r => r.HasOne<Division>().WithMany()
                        .HasForeignKey("Divisionid")
                        .HasConstraintName("fk_eventdivision_division"),
                    l => l.HasOne<Event>().WithMany()
                        .HasForeignKey("Eventid")
                        .HasConstraintName("fk_eventdivision_event"),
                    j =>
                    {
                        j.HasKey("Eventid", "Divisionid").HasName("eventdivision_pk");
                        j.ToTable("eventdivision");
                        j.IndexerProperty<int>("Eventid").HasColumnName("eventid");
                        j.IndexerProperty<int>("Divisionid").HasColumnName("divisionid");
                    });
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

        modelBuilder.Entity<Injagroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("injagroup_pk");

            entity.ToTable("injagroup");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
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

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("true")
                .HasColumnName("active");
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
            entity.Property(e => e.Urlphoto)
                .HasMaxLength(200)
                .HasColumnName("urlphoto");
            entity.Property(e => e.Usertype)
                .HasMaxLength(20)
                .HasColumnName("usertype");

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

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => new { e.Eventid, e.Challengeid, e.Contenderid, e.Photographerid, e.Created }).HasName("photo_pk");

            entity.ToTable("photo");

            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Challengeid).HasColumnName("challengeid");
            entity.Property(e => e.Contenderid).HasColumnName("contenderid");
            entity.Property(e => e.Photographerid).HasColumnName("photographerid");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.Filename)
                .HasMaxLength(100)
                .HasColumnName("filename");
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
