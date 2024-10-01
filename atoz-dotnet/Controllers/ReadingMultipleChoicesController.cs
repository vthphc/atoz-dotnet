using atoz_dotnet.Database;
using atoz_dotnet.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace atoz_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadingMultipleChoicesController : ControllerBase
    {
        private readonly IMongoCollection<ReadingMultipleChoices>? readingMultipleChoices;

        public ReadingMultipleChoicesController(MongoDbService mongoDbService)
        {
            readingMultipleChoices = mongoDbService.GetDatabase().GetCollection<ReadingMultipleChoices>("readingmultiplechoices");
        }

        [HttpGet]
        public async Task<IEnumerable<ReadingMultipleChoices>> Get()
        {
            return await readingMultipleChoices.Find(FilterDefinition<ReadingMultipleChoices>.Empty).ToListAsync();
        }
    }
}
