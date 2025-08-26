public class WorkflowStep
{
    public int Id { get; set; }
    public int WorkflowId { get; set; }
    public string StepType { get; set; } = string.Empty; 
    public string ConfigJson { get; set; } = string.Empty; 
    public int OrderIndex { get; set; }
}
