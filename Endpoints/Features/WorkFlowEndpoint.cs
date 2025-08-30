using Microsoft.EntityFrameworkCore;

public class WorkFlowEndpoint : IEndpointDefinition
{
    public void RegisterEndpoints(WebApplication app)
    {
        var workflowGroup = app.MapGroup("/workflow").RequireAuthorization().WithTags("Workflow");

        workflowGroup.MapGet("/", async (WorkFlowAutomationDbContext db, int userId) =>
        {
            var workflows = await db.Workflow
                        .Include(x => x.Steps)
                        .Where(x => x.UserId == userId).ToListAsync();

            if (workflows.Count < 0)
                Results.NotFound("No workflow found");

            return workflows;
        }).WithSummary("Get WorkFlow")
        .WithDescription("Get all workflows");

        workflowGroup.MapGet("/byId", async (WorkFlowAutomationDbContext db, int workflowId) =>
        {
            var workflows = await db.Workflow
                    .Include(x => x.Steps)
                    .Where(x => x.Id == workflowId).FirstOrDefaultAsync();

            if (workflows == null)
                Results.NotFound("No workflow found");

            return workflows;
        }).WithSummary("Get workflow by id")
        .WithDescription("Get workflow by id");
        
        workflowGroup.MapPost("/", async (WorkFlowAutomationDbContext db, WorkflowDto workflowDto) =>
        {
            var workflow = new Workflow
            {
                Name = workflowDto.Name,
                UserId = workflowDto.UserId,
                Steps = workflowDto.Steps.Select(stepDto => new WorkflowStep
                {

                    StepType = stepDto.StepType,
                    ConfigJson = stepDto.ConfigJson,
                    OrderIndex = stepDto.OrderIndex

                }).ToList()
            };

            await db.Workflow.AddAsync(workflow);
            await db.SaveChangesAsync();

            // return workflows;
        }).WithSummary("create workflow")
        .WithDescription("create workflow");
        
        
    }
}