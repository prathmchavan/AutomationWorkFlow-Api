public class Workflow
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<WorkflowStep> Steps { get; set; } = new();
}
