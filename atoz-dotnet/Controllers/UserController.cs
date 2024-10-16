using atoz_dotnet.Database;
using atoz_dotnet.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace atoz_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> users;
        public UserController(MongoDbService mongoDbService)
        {
            users = mongoDbService.GetDatabase().GetCollection<User>("users");
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await users.Find(FilterDefinition<User>.Empty).ToListAsync();
        }

        //get user by id
        [HttpGet("{id}")]
        public async Task<User> Get(string id)
        {
            return await users.Find<User>(user => user.UserId == id).FirstOrDefaultAsync();
        }
    }
}
