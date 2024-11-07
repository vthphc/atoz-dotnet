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

        [HttpGet("getAllQuizzes")]
        public async Task<IEnumerable<SpeakingQuizzes>> Get()
        {
            return await speakingQuizzes.Find(FilterDefinition<SpeakingQuizzes>.Empty).ToListAsync();
        }

        [HttpGet("getQuizById/{id}")]
        public async Task<ActionResult<SpeakingQuizzes>> GetQuizById(string id)
        {
            var speakingQuiz = await speakingQuizzes.Find(sq => sq.Id == id).FirstOrDefaultAsync();

            if (speakingQuiz == null)
            {
                return NotFound();
            }

            return speakingQuiz;
        }

        [HttpPost("addQuiz")]
        public async Task<ActionResult<SpeakingQuizzes>> AddQuiz(SpeakingQuizzes speakingQuiz)
        {
            speakingQuiz.version = 0;
            await speakingQuizzes.InsertOneAsync(speakingQuiz);
            return speakingQuiz;
        }

        [HttpDelete("deleteQuizById/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var speakingQuiz = await speakingQuizzes.Find(sq => sq.Id == id).FirstOrDefaultAsync();

            if (speakingQuiz == null)
            {
                return NotFound();
            }

            await speakingQuizzes.DeleteOneAsync(sq => sq.Id == id);
            return NoContent();
        }

        //get with condition 
        //[HttpGet("getAllQuizzesWithCondition")]
        //public async Task<IEnumerable<SpeakingQuizzes>> GetWithCondition(string language, int userProgresion)
        //{
        //    var difficulty = 1;
        //    if (userProgresion >= 200)
        //    {
        //        difficulty = 2;
        //    }
        //    if (userProgresion >= 500)
        //    {
        //        difficulty = 3;
        //    }
        //    if (userProgresion >= 1000)
        //    {
        //        difficulty = 4;
        //    }

        //    return await speakingQuizzes.Find(sq => sq.Language == language && sq.Difficulty == difficulty).ToListAsync();
        //}
    }
}
