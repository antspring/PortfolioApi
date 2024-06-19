using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Project;

public class Team
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    public List<User.User> Users { get; set; }
}