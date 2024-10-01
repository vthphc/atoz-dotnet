using atoz_dotnet.Database;
using atoz_dotnet.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace atoz_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeakingQuizzesController : ControllerBase
    {
        private readonly IMongoCollection<SpeakingQuizzes>? speakingQuizzes;

        public SpeakingQuizzesController(MongoDbService mongoDbService)
        {
            speakingQuizzes = mongoDbService.GetDatabase().GetCollection<SpeakingQuizzes>("speakingquizzes");
        }

        [HttpGet]
        public async Task<IEnumerable<SpeakingQuizzes>> Get()
        {
            return await speakingQuizzes.Find(FilterDefinition<SpeakingQuizzes>.Empty).ToListAsync();
        }
    }
}
