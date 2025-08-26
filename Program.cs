using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        // Todo : remove this allow any origin line before deployment
        policy.AllowAnyOrigin(); 
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.MapOpenApi();
    app.MapScalarApiReference();
// }

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    await next();

    if (context.Response != null)
    {
        var corsHeaders = context.Response.Headers
            .Where(h => h.Key.StartsWith("Access-Control-", StringComparison.InvariantCultureIgnoreCase));
    }
});

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.RegisterEndpointExtension();

app.MapGet("/Health", () =>
{
    return Results.Ok("Healthy ❤️");
});

app.Run();