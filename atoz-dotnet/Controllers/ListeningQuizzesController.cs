using atoz_dotnet.Database;
using atoz_dotnet.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace atoz_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListeningQuizzesController : ControllerBase
    {
        private readonly IMongoCollection<ListeningQuizzes>? listeningQuizzes;

        public ListeningQuizzesController(MongoDbService mongoDbService)
        {
            listeningQuizzes = mongoDbService.GetDatabase().GetCollection<ListeningQuizzes>("listeningquizzes");
        }

        [HttpGet]
        public async Task<IEnumerable<ListeningQuizzes>> Get()
        {
            return await listeningQuizzes.Find(FilterDefinition<ListeningQuizzes>.Empty).ToListAsync();
        }
    }
}
