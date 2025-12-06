using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IndianBank.EntityModel;

public partial class BankingDbContext : DbContext
{
    public BankingDbContext()
    {
    }

    public BankingDbContext(DbContextOptions<BankingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountDetail> AccountDetails { get; set; }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<BranchDetail> BranchDetails { get; set; }

    public virtual DbSet<CardDetail> CardDetails { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<LoanDivision> LoanDivisions { get; set; }

    public virtual DbSet<Personal> Personals { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPayeeDetail> UserPayeeDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=suchi;Initial Catalog=BANKING_DB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountDetail>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__AccountD__349DA58639107D04");

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.AccountTypeId).HasColumnName("AccountTypeID");
            entity.Property(e => e.Balance)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(15, 2)");
            entity.Property(e => e.BranchId).HasColumnName("BranchID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.AccountType).WithMany(p => p.AccountDetails)
                .HasForeignKey(d => d.AccountTypeId)
                .HasConstraintName("FK__AccountDe__Accou__49C3F6B7");

            entity.HasOne(d => d.Branch).WithMany(p => p.AccountDetails)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK__AccountDe__Branc__4AB81AF0");

            entity.HasOne(d => d.User).WithMany(p => p.AccountDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__AccountDe__UserI__48CFD27E");
        });

        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.HasKey(e => e.AccountTypeId).HasName("PK__AccountT__8F95854F07F44CA7");

            entity.ToTable("AccountType");

            entity.Property(e => e.AccountTypeId)
                .ValueGeneratedNever()
                .HasColumnName("AccountTypeID");
            entity.Property(e => e.AccountTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Address__091C2A1B343E6DF1");

            entity.ToTable("Address");

            entity.Property(e => e.AddressId)
                .ValueGeneratedNever()
                .HasColumnName("AddressID");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Address__UserID__5165187F");
        });

        modelBuilder.Entity<BranchDetail>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PK__BranchDe__A1682FA54AC51550");

            entity.HasIndex(e => e.Ifsccode, "UQ__BranchDe__6C74377FC2B43891").IsUnique();

            entity.Property(e => e.BranchId)
                .ValueGeneratedNever()
                .HasColumnName("BranchID");
            entity.Property(e => e.BranchName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ifsccode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IFSCCode");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");

            entity.HasOne(d => d.Manager).WithMany(p => p.BranchDetails)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__BranchDet__Manag__44FF419A");
        });

        modelBuilder.Entity<CardDetail>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__CardDeta__55FECD8E55559490");

            entity.HasIndex(e => e.CardNumber, "UQ__CardDeta__A4E9FFE91C78A63F").IsUnique();

            entity.Property(e => e.CardId)
                .ValueGeneratedNever()
                .HasColumnName("CardID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CardType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Debit");
            entity.Property(e => e.Cvv)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("CVV");

            entity.HasOne(d => d.Account).WithMany(p => p.CardDetails)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__CardDetai__Accou__60A75C0F");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD74EF3C60");

            entity.ToTable("Department");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedNever()
                .HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LoanDivision>(entity =>
        {
            entity.HasKey(e => e.LoanTypeId).HasName("PK__LoanDivi__19466B4FEF4C8AB8");

            entity.ToTable("LoanDivision");

            entity.Property(e => e.LoanTypeId)
                .ValueGeneratedNever()
                .HasColumnName("LoanTypeID");
            entity.Property(e => e.InterestRate).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.LoanTypeName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MaxAmount).HasColumnType("decimal(15, 2)");
        });

        modelBuilder.Entity<Personal>(entity =>
        {
            entity.HasKey(e => e.PersonalId).HasName("PK__Personal__28343713E0A94684");

            entity.ToTable("Personal");

            entity.HasIndex(e => e.UserId, "UQ__Personal__1788CCAD613CFF8A").IsUnique();

            entity.Property(e => e.PersonalId)
                .ValueGeneratedNever()
                .HasColumnName("PersonalID");
            entity.Property(e => e.Aadhar)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Pan)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("PAN");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithOne(p => p.Personal)
                .HasForeignKey<Personal>(d => d.UserId)
                .HasConstraintName("FK__Personal__UserID__4E88ABD4");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A6BB8F71A");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Test__B2079BCDD7FD2632");

            entity.ToTable("Test");

            entity.Property(e => e.DepartmentId)
                .ValueGeneratedNever()
                .HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__55433A4BA6941AAC");

            entity.Property(e => e.TransactionId)
                .ValueGeneratedNever()
                .HasColumnName("TransactionID");
            entity.Property(e => e.Amount).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.FromAccountId).HasColumnName("FromAccountID");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ToAccountId).HasColumnName("ToAccountID");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TransactionTypeId).HasColumnName("TransactionTypeID");

            entity.HasOne(d => d.FromAccount).WithMany(p => p.TransactionFromAccounts)
                .HasForeignKey(d => d.FromAccountId)
                .HasConstraintName("FK__Transacti__FromA__59FA5E80");

            entity.HasOne(d => d.ToAccount).WithMany(p => p.TransactionToAccounts)
                .HasForeignKey(d => d.ToAccountId)
                .HasConstraintName("FK__Transacti__ToAcc__5AEE82B9");

            entity.HasOne(d => d.TransactionType).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.TransactionTypeId)
                .HasConstraintName("FK__Transacti__Trans__5BE2A6F2");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.TransactionTypeId).HasName("PK__Transact__20266CEB6C324BDA");

            entity.ToTable("TransactionType");

            entity.Property(e => e.TransactionTypeId)
                .ValueGeneratedNever()
                .HasColumnName("TransactionTypeID");
            entity.Property(e => e.TransactionName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC69CF2A4E");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E491301D3C").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Users)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Users__Departmen__3C69FB99");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleID__3D5E1FD2");
        });

        modelBuilder.Entity<UserPayeeDetail>(entity =>
        {
            entity.HasKey(e => e.PayeeId).HasName("PK__UserPaye__0BC3E4396E91703F");

            entity.Property(e => e.PayeeId)
                .ValueGeneratedNever()
                .HasColumnName("PayeeID");
            entity.Property(e => e.Ifsccode)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IFSCCode");
            entity.Property(e => e.PayeeAccountNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PayeeBankName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PayeeName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserPayeeDetails)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserPayee__UserI__5629CD9C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
