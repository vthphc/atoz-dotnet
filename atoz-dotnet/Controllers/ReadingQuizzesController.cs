using atoz_dotnet.Database;
using atoz_dotnet.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace atoz_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingQuizzesController : ControllerBase
    {
        private readonly IMongoCollection<ReadingQuizzes>? readingQuizzes;

        public ReadingQuizzesController(MongoDbService mongoDbService)
        {
            readingQuizzes = mongoDbService.GetDatabase().GetCollection<ReadingQuizzes>("readingquizzes");
        }

        [HttpGet]
        public async Task<IEnumerable<ReadingQuizzes>> Get()
        {
            return await readingQuizzes.Find(FilterDefinition<ReadingQuizzes>.Empty).ToListAsync();
        }
    }
}
