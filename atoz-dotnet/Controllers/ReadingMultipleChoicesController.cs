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

        [HttpGet("getAllQuizzes")]
        public async Task<IEnumerable<ReadingMultipleChoices>> Get()
        {
            return await readingMultipleChoices.Find(FilterDefinition<ReadingMultipleChoices>.Empty).ToListAsync();
        }

        //get by id
        [HttpGet("getQuizById/{id}")]
        public async Task<ActionResult<ReadingMultipleChoices>> GetQuizById(string id)
        {
            var readingMultipleChoice = await readingMultipleChoices.Find(rmc => rmc.Id == id).FirstOrDefaultAsync();

            if (readingMultipleChoice == null)
            {
                return NotFound();
            }

            return readingMultipleChoice;
        }

        //add quizz
        [HttpPost("addQuiz")]
        public async Task<ActionResult<ReadingMultipleChoices>> AddQuiz(ReadingMultipleChoices readingMultipleChoice)
        {
            readingMultipleChoice.DateCreated = DateTime.Now;
            readingMultipleChoice.version = 0;
            await readingMultipleChoices.InsertOneAsync(readingMultipleChoice);
            return readingMultipleChoice;
        }

        //delete quizz
        [HttpDelete("deleteQuizById/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var readingMultipleChoice = await readingMultipleChoices.Find(rmc => rmc.Id == id).FirstOrDefaultAsync();

            if (readingMultipleChoice == null)
            {
                return NotFound();
            }

            await readingMultipleChoices.DeleteOneAsync(rmc => rmc.Id == id);

            return NoContent();
        }
    }
}
