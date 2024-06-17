using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.User;

public class SocialNetwork
{
    [Key] public int Id { get; set; }
    public string Link { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}