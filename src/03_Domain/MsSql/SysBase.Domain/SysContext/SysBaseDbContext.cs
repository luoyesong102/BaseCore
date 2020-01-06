using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SysBase.Domain.Models
{
    /// <summary>
    /// 由于注入的问题 数据库连接先进行注入，延时加载微软已经不再启用（需要的话需要扩展引用包启动延迟加载https://blog.csdn.net/xhl_james/article/details/93136893）
    /// 封装会使用显示加载include
    /// </summary>
    public partial class SysBaseDbContext : DbContext
    {
        public SysBaseDbContext(DbContextOptions<SysBaseDbContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseSqlServer("Data Source=SC-201610011543\\MYDB;Initial Catalog=Sys_Base_Db;User ID=sa;Password=Jianglei105;MultipleActiveResultSets=True;");
            //}
            //var loggerFactory = new LoggerFactory();
            //loggerFactory.AddProvider(new EFLoggerProvider());
            //optionsBuilder.UseLoggerFactory(loggerFactory);


        }
        public virtual DbSet<SysIcon> SysIcon { get; set; }
        public virtual DbSet<SysMenu> SysMenu { get; set; }
        public virtual DbSet<SysPermission> SysPermission { get; set; }
        public virtual DbSet<SysRole> SysRole { get; set; }
        public virtual DbSet<SysRolePermissionMapping> SysRolePermissionMapping { get; set; }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<SysUserRoleMapping> SysUserRoleMapping { get; set; }

        #region DbQuery
        /// <summary>
        /// 
        /// </summary>
        public DbQuery<SysPermissionWithAssignProperty> SysPermissionWithAssignProperty { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbQuery<SysPermissionWithMenu> SysPermissionWithMenu { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysIcon>(entity =>
            {
                entity.ToTable("Sys_Icon");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.Custom).HasMaxLength(60);

                entity.Property(e => e.Size).HasMaxLength(20);
            });

            modelBuilder.Entity<SysMenu>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Sys_Menu");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Alias).HasMaxLength(255);

                entity.Property(e => e.BeforeCloseFun).HasMaxLength(255);

                entity.Property(e => e.Component).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(800);

                entity.Property(e => e.Icon).HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Url).HasMaxLength(255);
            });
            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Sys_User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(800);

                entity.Property(e => e.DisplayName).HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(255);


            });
            modelBuilder.Entity<SysRole>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Sys_Role");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);


            });
            modelBuilder.Entity<SysPermission>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Sys_Permission");

                entity.Property(e => e.Id)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();

                entity.Property(e => e.ActionCode)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.MenuGu)
                    .WithMany(p => p.SysPermission)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_DncPermission_DncMenu_MenuId");
            });

         

            modelBuilder.Entity<SysRolePermissionMapping>(entity =>
            {
                entity.HasKey(x => new
                {
                    x.RoleId,
                    x.PermissionId
                });
                entity.ToTable("Sys_RolePermissionMapping");

                entity.Property(e => e.RoleId).HasMaxLength(50);

                entity.Property(e => e.PermissionId).HasMaxLength(20);

                entity.HasOne(d => d.PermissionCodeNavigation)
                    .WithMany(p => p.SysRolePermissionMapping)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("FK_DncRolePermissionMapping_DncPermission_PermissionId");

                entity.HasOne(d => d.RoleCodeNavigation)
                    .WithMany(p => p.SysRolePermissionMapping)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_DncRolePermissionMapping_DncRole_RoleId");
            });

          

            modelBuilder.Entity<SysUserRoleMapping>(entity =>
            {
                entity.HasKey(x => new
                {
                    x.RoleId,
                    x.UserId
                });

                entity.ToTable("Sys_UserRoleMapping");

                entity.Property(e => e.RoleId).HasMaxLength(50);
                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.HasOne(x => x.RoleCodeNavigation)
                   .WithMany(x => x.SysUserRoleMapping)
                   .HasForeignKey(x => x.RoleId)
                   .HasConstraintName("FK_Sys_UserRoleMapping_RoleId");

                entity.HasOne(x => x.UserGu)
               .WithMany(x => x.SysUserRoleMapping)
               .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_Sys_UserRoleMapping_UserId");

            });
        }


    }
}
