using AdminInteriorDesignStudioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminInteriorDesignStudioApi.Context;

public class AdminStudioContext : DbContext
{
    public AdminStudioContext(DbContextOptions<AdminStudioContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<ArchivedOrdersModel> FinishedOrders { get; set; }
    public DbSet<ActiveOrdersModel> Orders { get; set; }
}