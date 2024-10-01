using atoz_dotnet.Database;
using atoz_dotnet.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace atoz_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StageController : ControllerBase
    {
        private readonly IMongoCollection<Stage> stages;
        public StageController(MongoDbService mongoDbService)
        {
            stages = mongoDbService.GetDatabase().GetCollection<Stage>("stages");
        }

        [HttpGet]
        public async Task<IEnumerable<Stage>> Get()
        {
            return await stages.Find(FilterDefinition<Stage>.Empty).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Stage stage)
        {
            await stages.InsertOneAsync(stage);
            return CreatedAtAction(nameof(Get), new { id = stage.Id }, stage);
        }
    }
}
