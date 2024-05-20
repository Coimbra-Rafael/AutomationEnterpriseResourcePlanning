using AutomationEnterpriseResourcePlanning.Api.Models;

using Microsoft.EntityFrameworkCore;

namespace AutomationEnterpriseResourcePlanning.Api.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Customers> People { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option): base(option){}


}
