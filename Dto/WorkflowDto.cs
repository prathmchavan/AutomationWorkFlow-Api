public class WorkflowDto
{
    public string Name { get; set; } = string.Empty;
    public int UserId { get; set; }
    public List<WorkflowStepDto> Steps { get; set; } = new();
}
