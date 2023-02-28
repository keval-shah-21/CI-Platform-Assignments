using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CI_Platform.Entities.DataModels;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<CmsPage> CmsPages { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Mission> Missions { get; set; }

    public virtual DbSet<MissionSkill> MissionSkills { get; set; }

    public virtual DbSet<MissionTheme> MissionThemes { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSkill> UserSkills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:CI_Platform");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__admin__43AA4141ED4A941E");

            entity.ToTable("admin");

            entity.Property(e => e.AdminId)
                .ValueGeneratedOnAdd()
                .HasColumnName("admin_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.BannerId).HasName("PK__banner__10373C3447B822CA");

            entity.ToTable("banner");

            entity.Property(e => e.BannerId).HasColumnName("banner_id");
            entity.Property(e => e.BannerImage)
                .IsUnicode(false)
                .HasColumnName("banner_image");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.SortOrder)
                .HasDefaultValueSql("((0))")
                .HasColumnName("sort_order");
            entity.Property(e => e.Title)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__city__031491A82BB20BEB");

            entity.ToTable("city");

            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CityName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city_name");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(d => d.Country).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__city__country_id__2BFE89A6");
        });

        modelBuilder.Entity<CmsPage>(entity =>
        {
            entity.HasKey(e => e.CmsPageId).HasName("PK__cms_page__B46D5B527714D802");

            entity.ToTable("cms_page");

            entity.Property(e => e.CmsPageId).HasColumnName("cms_page_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__country__7E8CD055261DCFC7");

            entity.ToTable("country");

            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("country_name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Iso)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("ISO");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<Mission>(entity =>
        {
            entity.HasKey(e => e.MissionId).HasName("PK__mission__B5419AB25EFDC0A4");

            entity.ToTable("mission");

            entity.Property(e => e.MissionId).HasColumnName("mission_id");
            entity.Property(e => e.Availability).HasColumnName("availability");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.MissionCity).HasColumnName("mission_city");
            entity.Property(e => e.MissionCountry).HasColumnName("mission_country");
            entity.Property(e => e.MissionThemeId).HasColumnName("mission_theme_id");
            entity.Property(e => e.MissionType).HasColumnName("mission_type");
            entity.Property(e => e.OrganizationDetails)
                .HasColumnType("text")
                .HasColumnName("organization_details");
            entity.Property(e => e.OrganizationName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("organization_name");
            entity.Property(e => e.RegistrationDeadline).HasColumnName("registration_deadline");
            entity.Property(e => e.ShortDescription)
                .HasColumnType("text")
                .HasColumnName("short_description");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.TotalSeats).HasColumnName("total_seats");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<MissionSkill>(entity =>
        {
            entity.HasKey(e => e.MissionSkillId).HasName("PK__mission___82712008CC35B3A7");

            entity.ToTable("mission_skill");

            entity.Property(e => e.MissionSkillId).HasColumnName("mission_skill_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.MissionId).HasColumnName("mission_id");
            entity.Property(e => e.SkillId).HasColumnName("skill_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<MissionTheme>(entity =>
        {
            entity.HasKey(e => e.MissionThemeId).HasName("PK__mission___4925C5ACD6D1CA54");

            entity.ToTable("mission_theme");

            entity.HasIndex(e => e.MissionThemeName, "UQ__mission___A8CFF958A8E88772").IsUnique();

            entity.Property(e => e.MissionThemeId).HasColumnName("mission_theme_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.MissionThemeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mission_theme_name");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("PK__skill__FBBA8379B987BB58");

            entity.ToTable("skill");

            entity.HasIndex(e => e.SkillName, "UQ__skill__73C038ADD606E8F2").IsUnique();

            entity.Property(e => e.SkillId).HasColumnName("skill_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.SkillName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("skill_name");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__user__B9BE370F59AC672E");

            entity.ToTable("user");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Avatar)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Department)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("department");
            entity.Property(e => e.Email)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("employee_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.LinkedInUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("linked_in_url");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("phone_number");
            entity.Property(e => e.ProfileText)
                .HasColumnType("text")
                .HasColumnName("profile_text");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.WhyIVolunteer)
                .HasColumnType("text")
                .HasColumnName("why_i_volunteer");

            entity.HasOne(d => d.City).WithMany(p => p.Users)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__user__city_id__30C33EC3");

            entity.HasOne(d => d.Country).WithMany(p => p.Users)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__user__country_id__31B762FC");
        });

        modelBuilder.Entity<UserSkill>(entity =>
        {
            entity.HasKey(e => e.UserSkillId).HasName("PK__user_ski__FD3B576B07389B1D");

            entity.ToTable("user_skill");

            entity.Property(e => e.UserSkillId).HasColumnName("user_skill_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.SkillId).HasColumnName("skill_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
