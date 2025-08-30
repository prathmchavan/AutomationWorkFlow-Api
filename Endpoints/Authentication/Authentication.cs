using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace WorkflowAutomation.Api.Authentications
{
    public class Authentications : IEndpointDefinition
    {
        public void RegisterEndpoints(WebApplication app)
        {
            var authGroup = app.MapGroup("/auth").WithTags("Authentication");

            authGroup.MapPost("/register", async (User input, WorkFlowAutomationDbContext db) =>
            {
                if (string.IsNullOrWhiteSpace(input.Name))
                    return Results.Problem("Name is required");

                if (string.IsNullOrWhiteSpace(input.Password))
                    return Results.Problem("Password is mandatory");

                var user = new User
                {
                    Name = input.Name,
                    Password = input.Password,
                    Email = input.Email
                };

                await db.User.AddAsync(user);
                await db.SaveChangesAsync();

                return Results.Created($"/auth/{user.Id}", user);
            }).WithDescription("Register")
            .WithSummary("Register User");

            authGroup.MapPost("/login", async (UserDto input , WorkFlowAutomationDbContext db) =>
            {
                if (input.Password == null)
                    Results.Problem("Password should be non null");

                if (input.Name == null)
                    Results.Problem("Name should be non null");

                var user = await db.User.FirstOrDefaultAsync(x => x.Name == input.Name);

                if (!string.Equals(user.Password, input.Password))
                    Results.Forbid();


                 var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(app.Configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: app.Configuration["JWT:ValidIssuer"],
                    audience: app.Configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Results.Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    userId = user.Id,
                    name = user.Name,
                    email = user.Email
                });

            }).WithDescription("Login")
            .WithSummary("Login the user");

        }
    }
}