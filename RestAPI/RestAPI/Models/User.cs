using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
namespace RestAPI.Models
{
    [CollectionName("Users")]
    public class User : MongoIdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? AssignedPsychiatristId { get; set; }
    }
}
