using Microsoft.EntityFrameworkCore;

namespace InjaData.Models
{
	public partial class dbContext : DbContext
	{
		public dbContext()
		{
		}

		public dbContext(DbContextOptions<dbContext> options)
				: base(options)
		{
		}

		public virtual DbSet<City> Cities { get; set; } = null!;
		public virtual DbSet<Country> Countries { get; set; } = null!;
		public virtual DbSet<Doctype> Doctypes { get; set; } = null!;
		public virtual DbSet<Person> People { get; set; } = null!;
		public virtual DbSet<Personaddress> Personaddresses { get; set; } = null!;

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseNpgsql(Helper.CS);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<City>(entity =>
			{
				entity.HasKey(e => e.Idcity)
									.HasName("city_pk");

				entity.ToTable("city");

				entity.Property(e => e.Idcity).HasColumnName("idcity");

				entity.Property(e => e.Cityname)
									.HasMaxLength(100)
									.HasColumnName("cityname");

				entity.Property(e => e.Idcountry).HasColumnName("idcountry");

				entity.HasOne(d => d.IdcountryNavigation)
									.WithMany(p => p.Cities)
									.HasForeignKey(d => d.Idcountry)
									.OnDelete(DeleteBehavior.ClientSetNull)
									.HasConstraintName("fk_city_country");
			});

			modelBuilder.Entity<Country>(entity =>
			{
				entity.ToTable("country");

				entity.Property(e => e.Id)
									.ValueGeneratedNever()
									.HasColumnName("id");

				entity.Property(e => e.Countryname)
									.HasMaxLength(100)
									.HasColumnName("countryname");
			});

			modelBuilder.Entity<Doctype>(entity =>
			{
				entity.HasKey(e => e.Docid)
									.HasName("doctype_pk");

				entity.ToTable("doctype");

				entity.Property(e => e.Docid)
									.ValueGeneratedNever()
									.HasColumnName("docid");

				entity.Property(e => e.Docname)
									.HasMaxLength(50)
									.HasColumnName("docname");
			});

			modelBuilder.Entity<Person>(entity =>
			{
				entity.HasKey(e => e.Userid)
									.HasName("person_pk");

				entity.ToTable("person");

				entity.Property(e => e.Userid).HasColumnName("userid");

				entity.Property(e => e.Active).HasColumnName("active");

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

				entity.Property(e => e.Pass)
									.HasMaxLength(100)
									.HasColumnName("pass");

				entity.Property(e => e.Usertype)
									.HasMaxLength(20)
									.HasColumnName("usertype");

				entity.HasOne(d => d.Doc)
									.WithMany(p => p.People)
									.HasForeignKey(d => d.Docid)
									.OnDelete(DeleteBehavior.ClientSetNull)
									.HasConstraintName("fk_person_doctype");
			});

			modelBuilder.Entity<Personaddress>(entity =>
			{
				entity.HasKey(e => e.Userid)
									.HasName("personaddress_pk");

				entity.ToTable("personaddress");

				entity.Property(e => e.Userid)
									.ValueGeneratedNever()
									.HasColumnName("userid");

				entity.Property(e => e.Idcity).HasColumnName("idcity");

				entity.Property(e => e.Number)
									.HasMaxLength(10)
									.HasColumnName("number");

				entity.Property(e => e.Street)
									.HasMaxLength(100)
									.HasColumnName("street");

				entity.HasOne(d => d.IdcityNavigation)
									.WithMany(p => p.Personaddresses)
									.HasForeignKey(d => d.Idcity)
									.OnDelete(DeleteBehavior.ClientSetNull)
									.HasConstraintName("fk_personaddress_city");

				entity.HasOne(d => d.User)
									.WithOne(p => p.Personaddress)
									.HasForeignKey<Personaddress>(d => d.Userid)
									.OnDelete(DeleteBehavior.ClientSetNull)
									.HasConstraintName("fk_personaddress_person");
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
