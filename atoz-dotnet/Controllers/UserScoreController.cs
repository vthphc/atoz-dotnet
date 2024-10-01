using atoz_dotnet.Database;
using atoz_dotnet.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace atoz_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserScoreController : ControllerBase
    {
        private readonly IMongoCollection<UserScore> userscores;
        public UserScoreController(MongoDbService mongoDbService)
        {
            userscores = mongoDbService.GetDatabase().GetCollection<UserScore>("userscores");
        }

        [HttpGet]
        public async Task<IEnumerable<UserScore>> Get()
        {
            return await userscores.Find(FilterDefinition<UserScore>.Empty).ToListAsync();
        }
    }
}
