using Microsoft.EntityFrameworkCore;

public class WorkFlowAutomationDbContext : DbContext
{
    public virtual DbSet<Workflow> Workflows { get; set; }
    public virtual DbSet<WorkflowStep> WorkflowSteps { get; set; }
}