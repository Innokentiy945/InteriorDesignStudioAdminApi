using AdminInteriorDesignStudioApi.Context;
using AdminInteriorDesignStudioApi.Repository;
using Microsoft.EntityFrameworkCore;
using OpenApiInfo = Microsoft.OpenApi.Models.OpenApiInfo;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policyBuilder => policyBuilder.WithOrigins("http://localhost:62200/").AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddScoped<IAdminRepository, AdminService>();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Admin Studio API", Version = "v1" });
});

builder.Services.AddDbContext<AdminStudioContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Orders.db"));
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowSpecificOrigin");
app.UseResponseCaching();
app.UseStaticFiles();
app.UseEndpoints(endpoints =>
{
    endpoints.MapSwagger();
    endpoints.MapControllers().RequireCors("AllowSpecificOrigin");
});
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}").WithStaticAssets();
app.Run();