using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace RestAPI.Models
{
    [CollectionName("Psychiatrist")]
    public class Psychiatrist : MongoIdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatientAddCode { get; set; }
    }
}
