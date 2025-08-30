using Microsoft.EntityFrameworkCore;

public class WorkFlowAutomationDbContext(DbContextOptions<WorkFlowAutomationDbContext> options) : DbContext(options)
{
    public virtual DbSet<Workflow> Workflow { get; set; }
    public virtual DbSet<WorkflowStep> WorkflowStep { get; set; }
    public virtual DbSet<User> User { get; set; }
}