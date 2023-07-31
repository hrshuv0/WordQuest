using Core.Common.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity;

public class ApplicationUser : IdentityUser
{
    public string? DisplayName { get; set; }

    public DateTime CreatedTime { get; set; }
    public DateTime LastActive { get; set; }
    public Status Status { get; set; }

    public ApplicationUser()
    {
        CreatedTime = DateTime.Now;
        LastActive = DateTime.Now;
        Status = Status.Active;
    }
}