using InteriorDesignStudioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InteriorDesignStudioAdminApi.Context;

public class AdminStudioContext : DbContext
{
    public AdminStudioContext(DbContextOptions<AdminStudioContext> options) : base(options) { }
    public DbSet<OrderModel> FinishedOrders { get; set; }
}