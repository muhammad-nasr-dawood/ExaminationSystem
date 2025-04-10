using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ExaminationSystem.Core.Models;

public partial class ExaminationDbContext : DbContext
{
    public ExaminationDbContext()
    {
    }

    public ExaminationDbContext(DbContextOptions<ExaminationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnswerTf> AnswerTfs { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<BranchDept> BranchDepts { get; set; }

    public virtual DbSet<Choice> Choices { get; set; }

    public virtual DbSet<Configuration> Configurations { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<ExamModel> ExamModels { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Intake> Intakes { get; set; }

    public virtual DbSet<IntakeDeptCourse> IntakeDeptCourses { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<MultiChoiceResponse> MultiChoiceResponses { get; set; }

    public virtual DbSet<Pool> Pools { get; set; }

    public virtual DbSet<PoolQuestion> PoolQuestions { get; set; }

    public virtual DbSet<ProfileImage> ProfileImages { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<StaffBranchDepartmentManagement> StaffBranchDepartmentManagements { get; set; }

    public virtual DbSet<StaffBranchDepartmentWorksFor> StaffBranchDepartmentWorksFors { get; set; }

    public virtual DbSet<StaffBranchIntakeDepartmentCourseTeach> StaffBranchIntakeDepartmentCourseTeaches { get; set; }

    public virtual DbSet<StaffBranchManage> StaffBranchManages { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentExamModel> StudentExamModels { get; set; }

    public virtual DbSet<StudentIntakeBranchDepartmentStudy> StudentIntakeBranchDepartmentStudies { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<Topic> Topics { get; set; }

    public virtual DbSet<TrueFalseResponse> TrueFalseResponses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:dod.database.windows.net,1433;Initial Catalog=ExaminationDB;Persist Security Info=False;User ID=DOD;Password=Dawood@72;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnswerTf>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("AnswerTF");

            entity.HasOne(d => d.Question).WithMany()
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_AnswerTF_Quesstion");
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Branch_Id");

            entity.HasIndex(e => e.ZipCode, "UQ_Branches_ZipCode").IsUnique();

            entity.Property(e => e.StreetNo).HasMaxLength(5);
            entity.Property(e => e.ZipCode)
                .HasMaxLength(6)
                .IsUnicode(false);

            entity.HasOne(d => d.ZipCodeNavigation).WithOne(p => p.Branch)
                .HasForeignKey<Branch>(d => d.ZipCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Branches_Locations");
        });

        modelBuilder.Entity<BranchDept>(entity =>
        {
            entity.HasKey(e => new { e.BranchId, e.DeptId });

            entity.HasOne(d => d.Branch).WithMany(p => p.BranchDepts)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchDepts_Branchs");

            entity.HasOne(d => d.Dept).WithMany(p => p.BranchDepts)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchDepts_Depts");
        });

        modelBuilder.Entity<Choice>(entity =>
        {
            entity.HasKey(e => new { e.Index, e.QuestionId });

            entity.Property(e => e.Index).HasColumnName("Index_");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");

            entity.HasOne(d => d.Question).WithMany(p => p.Choices)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_Choices_Questions");
        });

        modelBuilder.Entity<Configuration>(entity =>
        {
            entity.HasKey(e => e.PoolId).HasName("PK__Configur__EEFA8AEFB7572F0D");

            entity.Property(e => e.PoolId).ValueGeneratedNever();
            entity.Property(e => e.CanModify).HasDefaultValue(true);

            entity.HasOne(d => d.Pool).WithOne(p => p.Configuration)
                .HasForeignKey<Configuration>(d => d.PoolId)
                .HasConstraintName("FK_Configurations_Pool");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Courses__3214EC07EDD3BE86");

            entity.Property(e => e.Name).HasMaxLength(30);

            entity.HasMany(d => d.Tops).WithMany(p => p.Crs)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseTopic",
                    r => r.HasOne<Topic>().WithMany()
                        .HasForeignKey("TopId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Course_To__Top_I__498EEC8D"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CrsId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Course_To__Crs_I__489AC854"),
                    j =>
                    {
                        j.HasKey("CrsId", "TopId").HasName("Set_PK");
                        j.ToTable("Course_Topics");
                        j.IndexerProperty<int>("CrsId").HasColumnName("Crs_Id");
                        j.IndexerProperty<int>("TopId").HasColumnName("Top_Id");
                    });
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Dept_Id");

            entity.HasIndex(e => e.Disc, "UQ_Dept_Disc").IsUnique();

            entity.HasIndex(e => e.Name, "UQ_Dept_Name").IsUnique();

            entity.Property(e => e.Disc).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(10);
        });

        modelBuilder.Entity<ExamModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ExamMode__3214EC07BDACB61A");

            entity.HasOne(d => d.Pool).WithMany(p => p.ExamModels)
                .HasForeignKey(d => d.PoolId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ExamModel_Pool");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Images__3214EC07894DBE8D");

            entity.Property(e => e.Id)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Question).WithMany(p => p.Images)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Images_Questions");
        });

        modelBuilder.Entity<Intake>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Intakes__3214EC07B8E02774");

            entity.ToTable(tb => tb.HasTrigger("OnAddIntake"));

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsRunning).HasDefaultValue((byte)1);
        });

        modelBuilder.Entity<IntakeDeptCourse>(entity =>
        {
            entity.HasKey(e => new { e.DeptId, e.CourseId, e.IntakeId }).HasName("PK_DeptCourses");

            entity.HasOne(d => d.Course).WithMany(p => p.IntakeDeptCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeptCourses_Course");

            entity.HasOne(d => d.Dept).WithMany(p => p.IntakeDeptCourses)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeptCourses_Dept");

            entity.HasOne(d => d.Intake).WithMany(p => p.IntakeDeptCourses)
                .HasForeignKey(d => d.IntakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeptCourses_Intakes");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.ZipCode).HasName("PK__Location__2CC2CDB9DE47BF7B");

            entity.Property(e => e.ZipCode)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Governate)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MultiChoiceResponse>(entity =>
        {
            entity.HasKey(e => new { e.StdSsn, e.ExamId, e.QuestionId }).HasName("PK__MultiCho__E93C77EF8DFCE75E");

            entity.ToTable("MultiChoiceResponse");

            entity.Property(e => e.StdSsn).HasColumnName("STD_SSN");
            entity.Property(e => e.ExamId).HasColumnName("Exam_ID");
            entity.Property(e => e.QuestionId).HasColumnName("Question_ID");
            entity.Property(e => e.AnswerId).HasColumnName("Answer_ID");

            entity.HasOne(d => d.Exam).WithMany(p => p.MultiChoiceResponses)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MultiChoiceResponse_Exam");

            entity.HasOne(d => d.Question).WithMany(p => p.MultiChoiceResponses)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MultiChoiceResponse_Question");

            entity.HasOne(d => d.StdSsnNavigation).WithMany(p => p.MultiChoiceResponses)
                .HasForeignKey(d => d.StdSsn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MultiChoiceResponse_Student");

            entity.HasOne(d => d.Choice).WithMany(p => p.MultiChoiceResponses)
                .HasForeignKey(d => new { d.AnswerId, d.QuestionId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MultiChoiceResponse_Answer");
        });

        modelBuilder.Entity<Pool>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pools__3214EC075BC0AABE");

            entity.Property(e => e.IsActive).HasDefaultValue((byte)1);
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Branch).WithMany(p => p.Pools)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Configurations_Branch");

            entity.HasOne(d => d.Course).WithMany(p => p.Pools)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Configurations_Course");

            entity.HasOne(d => d.Dept).WithMany(p => p.Pools)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Configurations_Dept");

            entity.HasOne(d => d.Intake).WithMany(p => p.Pools)
                .HasForeignKey(d => d.IntakeId)
                .HasConstraintName("FK_Pools_Intake");

            entity.HasOne(d => d.Staff).WithMany(p => p.Pools)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Configurations_Staff");
        });

        modelBuilder.Entity<PoolQuestion>(entity =>
        {
            entity.HasKey(e => new { e.PoolId, e.QuestionId }).HasName("PK");

            entity.ToTable("PoolQuestion");

            entity.Property(e => e.IsIncluded).HasDefaultValue((byte)0);

            entity.HasOne(d => d.Pool).WithMany(p => p.PoolQuestions)
                .HasForeignKey(d => d.PoolId)
                .HasConstraintName("FK_PoolQuestion_Pool");

            entity.HasOne(d => d.Question).WithMany(p => p.PoolQuestions)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PoolQuestion_Question");
        });

        modelBuilder.Entity<ProfileImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__ProfileI__7516F70C02E802E9");

            entity.Property(e => e.ImageId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3214EC07A202E58A");

            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Staff).WithMany(p => p.Questions)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK_Questions_Staff");

            entity.HasOne(d => d.Topic).WithMany(p => p.Questions)
                .HasForeignKey(d => d.TopicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Question_Topic");

            entity.HasMany(d => d.ExamModels).WithMany(p => p.Questions)
                .UsingEntity<Dictionary<string, object>>(
                    "ExamQuestion",
                    r => r.HasOne<ExamModel>().WithMany()
                        .HasForeignKey("ExamModelId")
                        .HasConstraintName("FK_ExamQuestion_Exam"),
                    l => l.HasOne<Question>().WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ExamQuestion_Questions"),
                    j =>
                    {
                        j.HasKey("QuestionId", "ExamModelId");
                        j.ToTable("ExamQuestion");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC070065C3C5");

            entity.HasIndex(e => e.Name, "UQ__Roles__737584F6F41081FC").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Session");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Ssn).HasName("PK__Staff__CA1E8E3D2E07F881");

            entity.ToTable(tb => tb.HasTrigger("trg_EnsureStaffUserIsnotStudentUser"));

            entity.Property(e => e.Ssn)
                .ValueGeneratedNever()
                .HasColumnName("SSN");
            entity.Property(e => e.Salary)
                .HasDefaultValue(10000m)
                .HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.SsnNavigation).WithOne(p => p.Staff)
                .HasForeignKey<Staff>(d => d.Ssn)
                .HasConstraintName("FK__Staff__SSN__57DD0BE4");

            entity.HasMany(d => d.Roles).WithMany(p => p.StaffSsns)
                .UsingEntity<Dictionary<string, object>>(
                    "StaffRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__StaffRole__RoleI__65370702"),
                    l => l.HasOne<Staff>().WithMany()
                        .HasForeignKey("StaffSsn")
                        .HasConstraintName("FK__StaffRole__Staff__6442E2C9"),
                    j =>
                    {
                        j.HasKey("StaffSsn", "RoleId").HasName("PK__StaffRol__8D7DEA63DB110087");
                        j.ToTable("StaffRoles");
                        j.IndexerProperty<long>("StaffSsn").HasColumnName("StaffSSN");
                    });
        });

        modelBuilder.Entity<StaffBranchDepartmentManagement>(entity =>
        {
            entity.HasKey(e => new { e.BranchId, e.DepartmentId });

            entity.ToTable("StaffBranchDepartmentManagement");

            entity.HasIndex(e => e.StaffSsn, "UQ_StaffSSN").IsUnique();

            entity.Property(e => e.HiringDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.StaffSsn).HasColumnName("StaffSSN");

            entity.HasOne(d => d.Branch).WithMany(p => p.StaffBranchDepartmentManagements)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchDepartmentManagement_Branch");

            entity.HasOne(d => d.Department).WithMany(p => p.StaffBranchDepartmentManagements)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchDepartmentManagement_Departments");

            entity.HasOne(d => d.StaffSsnNavigation).WithOne(p => p.StaffBranchDepartmentManagement)
                .HasForeignKey<StaffBranchDepartmentManagement>(d => d.StaffSsn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchDepartmentManagement_Staff");

            entity.HasOne(d => d.BranchDept).WithOne(p => p.StaffBranchDepartmentManagement)
                .HasForeignKey<StaffBranchDepartmentManagement>(d => new { d.BranchId, d.DepartmentId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchDepartmentManagement_BranchDepts");
        });

        modelBuilder.Entity<StaffBranchDepartmentWorksFor>(entity =>
        {
            entity.HasKey(e => new { e.StaffSsn, e.BranchId, e.DepartmentId });

            entity.ToTable("StaffBranchDepartmentWorksFor");

            entity.Property(e => e.StaffSsn).HasColumnName("StaffSSN");
            entity.Property(e => e.HiringDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Branch).WithMany(p => p.StaffBranchDepartmentWorksFors)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchDepartmentWorksFor_Branch");

            entity.HasOne(d => d.Department).WithMany(p => p.StaffBranchDepartmentWorksFors)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchDepartmentWorksFor_Departments");

            entity.HasOne(d => d.StaffSsnNavigation).WithMany(p => p.StaffBranchDepartmentWorksFors)
                .HasForeignKey(d => d.StaffSsn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchDepartmentWorksFor_Staff");

            entity.HasOne(d => d.BranchDept).WithMany(p => p.StaffBranchDepartmentWorksFors)
                .HasForeignKey(d => new { d.BranchId, d.DepartmentId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchDepartmentWorksFor_BranchDepts");
        });

        modelBuilder.Entity<StaffBranchIntakeDepartmentCourseTeach>(entity =>
        {
            entity.HasKey(e => new { e.StaffSsn, e.BranchId, e.DepartmentId, e.CourseId, e.IntakeId });

            entity.ToTable("StaffBranchIntakeDepartmentCourseTeach");

            entity.Property(e => e.StaffSsn).HasColumnName("StaffSSN");
            entity.Property(e => e.EndingDate).HasColumnType("datetime");
            entity.Property(e => e.StartingDate).HasColumnType("datetime");

            entity.HasOne(d => d.Branch).WithMany(p => p.StaffBranchIntakeDepartmentCourseTeaches)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchIntakeDepartmentCourseTeach_Branch");

            entity.HasOne(d => d.Course).WithMany(p => p.StaffBranchIntakeDepartmentCourseTeaches)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchIntakeDepartmentCourseTeach_Courses");

            entity.HasOne(d => d.Department).WithMany(p => p.StaffBranchIntakeDepartmentCourseTeaches)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchIntakeDepartmentCourseTeach_Departments");

            entity.HasOne(d => d.Intake).WithMany(p => p.StaffBranchIntakeDepartmentCourseTeaches)
                .HasForeignKey(d => d.IntakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchIntakeDepartmentCourseTeach_Intakes");

            entity.HasOne(d => d.StaffSsnNavigation).WithMany(p => p.StaffBranchIntakeDepartmentCourseTeaches)
                .HasForeignKey(d => d.StaffSsn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchINtakeDepartmentCourseTeach_Staff");

            entity.HasOne(d => d.BranchDept).WithMany(p => p.StaffBranchIntakeDepartmentCourseTeaches)
                .HasForeignKey(d => new { d.BranchId, d.DepartmentId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchIntakeDepartmentCourseTeach_BranchDepts");

            entity.HasOne(d => d.IntakeDeptCourse).WithMany(p => p.StaffBranchIntakeDepartmentCourseTeaches)
                .HasForeignKey(d => new { d.DepartmentId, d.CourseId, d.IntakeId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchIntakeDepartmentCourseTeach_IntakeDeptCourses");
        });

        modelBuilder.Entity<StaffBranchManage>(entity =>
        {
            entity.HasKey(e => e.StaffSsn);

            entity.ToTable("StaffBranchManage");

            entity.HasIndex(e => e.BranchId, "UQ_StaffBranchManage_Branch").IsUnique();

            entity.Property(e => e.StaffSsn)
                .ValueGeneratedNever()
                .HasColumnName("StaffSSN");
            entity.Property(e => e.HiringDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Branch).WithOne(p => p.StaffBranchManage)
                .HasForeignKey<StaffBranchManage>(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchManage_Branch");

            entity.HasOne(d => d.StaffSsnNavigation).WithOne(p => p.StaffBranchManage)
                .HasForeignKey<StaffBranchManage>(d => d.StaffSsn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StaffBranchManage_Staff");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Ssn).HasName("PK__Students__CA1E8E3D46A0974A");

            entity.ToTable(tb => tb.HasTrigger("trg_EnsureStudentUserIsnotStaffUser"));

            entity.Property(e => e.Ssn)
                .ValueGeneratedNever()
                .HasColumnName("SSN");
            entity.Property(e => e.Faculty)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gpa)
                .HasColumnType("decimal(4, 3)")
                .HasColumnName("GPA");
            entity.Property(e => e.GradYear).HasColumnName("Grad_Year");

            entity.HasOne(d => d.SsnNavigation).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.Ssn)
                .HasConstraintName("FK__Students__SSN__6AEFE058");
        });

        modelBuilder.Entity<StudentExamModel>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.ExamModelId }).HasName("PK_StudentExamModel_StudentId_ExamModelId");

            entity.ToTable("StudentExamModel");

            entity.Property(e => e.Attendance).HasDefaultValue(false);
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.ExamModel).WithMany(p => p.StudentExamModels)
                .HasForeignKey(d => d.ExamModelId)
                .HasConstraintName("PK_StudentExamModel_ExamModels");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentExamModels)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PK_StudentExamModel_Students");
        });

        modelBuilder.Entity<StudentIntakeBranchDepartmentStudy>(entity =>
        {
            entity.HasKey(e => new { e.StudentSsn, e.IntakeId });

            entity.ToTable("StudentIntakeBranchDepartmentStudy", tb => tb.HasTrigger("Trg_StudentIntakeBranchDepartmentStudy_MaxIntakes"));

            entity.Property(e => e.StudentSsn).HasColumnName("StudentSSN");

            entity.HasOne(d => d.Branch).WithMany(p => p.StudentIntakeBranchDepartmentStudies)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentIntakeBranchDepartmentStudy_Branch");

            entity.HasOne(d => d.Department).WithMany(p => p.StudentIntakeBranchDepartmentStudies)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentIntakeBranchDepartmentStudy_Departments");

            entity.HasOne(d => d.Intake).WithMany(p => p.StudentIntakeBranchDepartmentStudies)
                .HasForeignKey(d => d.IntakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentIntakeBranchDepartmentStudy_Intakes");

            entity.HasOne(d => d.StudentSsnNavigation).WithMany(p => p.StudentIntakeBranchDepartmentStudies)
                .HasForeignKey(d => d.StudentSsn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentIntakeBranchDepartmentStudy_Students");

            entity.HasOne(d => d.BranchDept).WithMany(p => p.StudentIntakeBranchDepartmentStudies)
                .HasForeignKey(d => new { d.BranchId, d.DepartmentId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StudentIntakeBranchDepartmentStudy_BranchDepts");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Qid).HasName("PK__test__CAB1462B2453EC1D");

            entity.ToTable("test");

            entity.Property(e => e.Qid).HasColumnName("QId");
            entity.Property(e => e.PoolId).HasColumnName("poolId");
        });

        modelBuilder.Entity<Topic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Topics__3214EC076D2A933A");

            entity.HasIndex(e => e.Name, "UQ__Topics__737584F6809249A7").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<TrueFalseResponse>(entity =>
        {
            entity.HasKey(e => new { e.StdSsn, e.ExamId, e.QuestionId }).HasName("PK__TrueFals__E93C77EF202DAA7E");

            entity.ToTable("TrueFalseResponse");

            entity.Property(e => e.StdSsn).HasColumnName("STD_SSN");
            entity.Property(e => e.ExamId).HasColumnName("Exam_ID");
            entity.Property(e => e.QuestionId).HasColumnName("Question_ID");

            entity.HasOne(d => d.Exam).WithMany(p => p.TrueFalseResponses)
                .HasForeignKey(d => d.ExamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrueFalseResponse_Exam");

            entity.HasOne(d => d.Question).WithMany(p => p.TrueFalseResponses)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrueFalseResponse_Question");

            entity.HasOne(d => d.StdSsnNavigation).WithMany(p => p.TrueFalseResponses)
                .HasForeignKey(d => d.StdSsn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrueFalseResponse_Student");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Ssn).HasName("PK__Users__CA1E8E3D310E34C8");

            entity.HasIndex(e => e.Email, "UniqueEmailConstriant").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UniquePhoneConstriant").IsUnique();

            entity.Property(e => e.Ssn)
                .ValueGeneratedNever()
                .HasColumnName("SSN");
            entity.Property(e => e.Bd).HasColumnName("BD");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Fname).HasMaxLength(50);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ImageId)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Lname).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Salt).HasMaxLength(100);
            entity.Property(e => e.StreetNo)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.UserType)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ZipCode)
                .HasMaxLength(6)
                .IsUnicode(false);

            entity.HasOne(d => d.Image).WithMany(p => p.Users)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Users_Images_ImageId");

            entity.HasOne(d => d.ZipCodeNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.ZipCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Locations");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
