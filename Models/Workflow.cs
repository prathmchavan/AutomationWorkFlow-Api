public class Workflow
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public List<WorkflowStep> Steps { get; set; } = new();
}
