using AdminInteriorDesignStudioApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminInteriorDesignStudioApi.Context;

public class AdminStudioContext : DbContext
{
    public AdminStudioContext(DbContextOptions<AdminStudioContext> options) : base(options) { }
    public DbSet<ArchivedOrderModel> FinishedOrders { get; set; }
}