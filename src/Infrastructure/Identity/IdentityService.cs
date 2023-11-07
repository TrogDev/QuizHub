namespace QuizHub.Infrastructure.Identity;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuizHub.Application.Common.Abstractions.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> userManager;

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<bool> IsInRole(long userId, string role)
    {
        ApplicationUser? user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        return user is not null && await userManager.IsInRoleAsync(user, role);
    }
}
