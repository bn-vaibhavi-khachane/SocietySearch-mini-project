using Microsoft.AspNetCore.Identity;
using SocietySearch.Server.Model.Domain;

public class Manager: IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Society> Societies { get; set; } = new List<Society>();
}