using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace kyykt
{
    public partial class studentContext : DbContext
    {
        public studentContext()
        {
        }

        public studentContext(DbContextOptions<studentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClassMessage> ClassMessage { get; set; }
        public virtual DbSet<ClassSignIn> ClassSignIn { get; set; }
        public virtual DbSet<ClassTime> ClassTime { get; set; }
        public virtual DbSet<NeedToDo> NeedToDo { get; set; }
        public virtual DbSet<Notice> Notice { get; set; }
        public virtual DbSet<OpeningClass> OpeningClass { get; set; }
        public virtual DbSet<Selection> Selection { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentSignIn> StudentSignIn { get; set; }
        public virtual DbSet<TeaCourse> TeaCourse { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=123.207.235.148;User Id=waiwang;Password=pwd;Database=student");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClassMessage>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PRIMARY");

                entity.Property(e => e.MessageId).HasColumnType("int(11)");

                entity.Property(e => e.ClassId).HasColumnType("varchar(20)");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.MessageTime).HasColumnType("datetime");

                entity.Property(e => e.ReplyMessage).HasColumnType("varchar(500)");

                entity.Property(e => e.StudentId).HasColumnType("varchar(20)");
                entity.Property(e => e.MessageHead).HasColumnType("varchar(20)");
                entity.Property(e => e.HasReply)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<ClassSignIn>(entity =>
            {
                entity.HasKey(e => e.SignInCode)
                    .HasName("PRIMARY");

                entity.Property(e => e.SignInCode).HasColumnType("varchar(50)");

                entity.Property(e => e.ClassId).HasColumnType("varchar(20)");

                entity.Property(e => e.IsOverDue)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SignInDate).HasColumnType("datetime");

                entity.Property(e => e.SignNum)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<ClassTime>(entity =>
            {
                entity.HasKey(e => new { e.Time, e.Place })
                    .HasName("PRIMARY");

                entity.ToTable("classTime");

                entity.HasIndex(e => e.ClassId)
                    .HasName("FK_ID");

                entity.Property(e => e.Time)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Place)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.ClassId).HasColumnType("varchar(20)");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassTime)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("classTime_ibfk_1");
            });

            modelBuilder.Entity<NeedToDo>(entity =>
            {
                entity.HasKey(e => new { e.NeedToDoId, e.StudentId })
                    .HasName("PRIMARY");

                entity.ToTable("needToDo");

                entity.HasIndex(e => e.StudentId)
                    .HasName("FK_ID");

                entity.Property(e => e.NeedToDoId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.StudentId)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Content).HasColumnType("varchar(100)");

                entity.Property(e => e.Extra).HasColumnType("varchar(100)");

                entity.Property(e => e.Finish).HasColumnType("varchar(5)");

                entity.Property(e => e.Hide)
                    .HasColumnName("hide")
                    .HasColumnType("varchar(5)");

                entity.Property(e => e.Time).HasColumnType("varchar(20)");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.NeedToDo)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("needToDo_ibfk_1");
            });

            modelBuilder.Entity<Notice>(entity =>
            {
                entity.HasKey(e => new { e.NoticeId, e.ClassId })
                    .HasName("PRIMARY");

                entity.ToTable("notice");

                entity.HasIndex(e => e.ClassId)
                    .HasName("FK_ID");

                entity.Property(e => e.NoticeId)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ClassId)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Content).HasColumnType("varchar(100)");

                entity.Property(e => e.Head)
                    .HasColumnName("head")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Notice)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID");
            });

            modelBuilder.Entity<OpeningClass>(entity =>
            {
                entity.HasKey(e => e.ClassId)
                    .HasName("PRIMARY");

                entity.ToTable("openingClass");

                entity.HasIndex(e => e.CourseId)
                    .HasName("FK_ID");

                entity.Property(e => e.ClassId)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CloseClass).HasColumnType("varchar(5)");

                entity.Property(e => e.CourseId)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Times)
                    .HasColumnName("times")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.OpeningClass)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("openingClass_ibfk_1");
            });

            modelBuilder.Entity<Selection>(entity =>
            {
                entity.HasKey(e => new { e.ClassId, e.StudentId })
                    .HasName("PRIMARY");

                entity.ToTable("selection");

                entity.HasIndex(e => e.StudentId)
                    .HasName("FK_ID");

                entity.Property(e => e.ClassId)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.StudentId)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Examination)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

                entity.Property(e => e.ExaminationResults).HasColumnType("int(11)");

                entity.Property(e => e.TruancyTimes).HasColumnType("int(11)");

                entity.Property(e => e.UsualPerformance).HasColumnType("int(11)");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Selection)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("selection_ibfk_2");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Selection)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("selection_ibfk_1");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.StudentId)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name).HasColumnType("varchar(20)");

                entity.Property(e => e.Picture).HasColumnType("varchar(200)");

                entity.Property(e => e.Sex).HasColumnType("enum('男','女')");

                entity.Property(e => e.Tel).HasColumnType("varchar(20)");

                entity.Property(e => e.WxId).HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<StudentSignIn>(entity =>
            {
                entity.HasKey(e => new { e.SignInCode, e.StudentId })
                    .HasName("PRIMARY");

                entity.Property(e => e.SignInCode)
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.StudentId)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<TeaCourse>(entity =>
            {
                entity.HasKey(e => e.CourseId)
                    .HasName("PRIMARY");

                entity.ToTable("teaCourse");

                entity.HasIndex(e => e.TeacherId)
                    .HasName("FK_ID1");

                entity.Property(e => e.CourseId)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Credit).HasColumnType("int(11)");

                entity.Property(e => e.Hours).HasColumnType("int(11)");

                entity.Property(e => e.Name).HasColumnType("varchar(20)");

                entity.Property(e => e.TeacherId)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Way).HasColumnType("varchar(20)");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeaCourse)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ID1");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("teacher");

                entity.HasIndex(e => e.WxId)
                    .HasName("WxId")
                    .IsUnique();

                entity.Property(e => e.TeacherId)
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name).HasColumnType("varchar(20)");

                entity.Property(e => e.Occupation).HasColumnType("varchar(20)");

                entity.Property(e => e.Picture).HasColumnType("varchar(200)");

                entity.Property(e => e.Sex).HasColumnType("enum('男','女')");

                entity.Property(e => e.WxId).HasColumnType("varchar(50)");
                entity.Property(e => e.TeacherPasswd).HasColumnType("varchar(50)");
            });
        }


    }
}
