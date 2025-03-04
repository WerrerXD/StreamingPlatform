using System;

namespace User.Service.Domain.Entities;
public class AppUser
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public List<Role> Roles { get; set; }
    public Profile Profile { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}