using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyProductStore.Orders;
using MyProductStore.Products;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace MyProductStore.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class MyProductStoreDbContext :
    AbpDbContext<MyProductStoreDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

    public MyProductStoreDbContext(DbContextOptions<MyProductStoreDbContext> options)
        : base(options)
    {

    }
    
    // TODO: check the namespace
    protected bool IsAvailableFilterEnabled => DataFilter?.IsEnabled<IIsAvailable>() ?? false;

    protected override bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType)
    {
        if (typeof(IIsAvailable).IsAssignableFrom(typeof(TEntity)))
        {
            return true;
        }

        return base.ShouldFilterEntity<TEntity>(entityType);
    }

    protected override Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
    {
        var expression = base.CreateFilterExpression<TEntity>();

        if (typeof(IIsAvailable).IsAssignableFrom(typeof(TEntity)))
        {
            Expression<Func<TEntity, bool>> isAvailableFilter =
                e => !IsAvailableFilterEnabled || EF.Property<bool>(e, "IsAvailable");
            expression = expression == null 
                ? isAvailableFilter 
                : CombineExpressions(expression, isAvailableFilter);
        }

        return expression;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureIdentityServer();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        builder.Entity<Product>(b =>
        {
            b.ToTable(MyProductStoreConsts.DbTablePrefix + "Products", MyProductStoreConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            
            b.Property(p => p.Name).IsRequired().HasMaxLength(ProductConstants.NameMaxLength);
            b.Property(p => p.Price).IsRequired().HasColumnType("decimal(10,4)");

            b.HasIndex(q => q.Name);
        });
        
        builder.Entity<Order>(b =>
        {
            b.ToTable(MyProductStoreConsts.DbTablePrefix + "Orders", MyProductStoreConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.Property(q => q.Date).IsRequired();
            b.Property(q => q.Status).IsRequired();
            b.Property(q => q.CustomerId).IsRequired();

            b.HasOne<IdentityUser>().WithMany().HasForeignKey(q => q.CustomerId).IsRequired();
        });

        builder.Entity<OrderLine>(b =>
        {
            b.ToTable(MyProductStoreConsts.DbTablePrefix + "OrderLines", MyProductStoreConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.Property(q => q.Quantity).IsRequired();
            b.HasKey(op => new {op.OrderId, op.ProductId});

            b.HasOne<Order>().WithMany(q => q.OrderLines).HasForeignKey(q => q.OrderId);
            b.HasOne<Product>().WithMany().HasForeignKey(q => q.ProductId);
        });
    }
}
