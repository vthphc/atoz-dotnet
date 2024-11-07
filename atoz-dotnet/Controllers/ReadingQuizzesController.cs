using atoz_dotnet.Database;
using atoz_dotnet.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using static System.Net.Mime.MediaTypeNames;
using System;

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

        [HttpGet("getAllQuizzes")]
        public async Task<IEnumerable<ReadingQuizzes>> Get()
        {
            return await readingQuizzes.Find(FilterDefinition<ReadingQuizzes>.Empty).ToListAsync();
        }

        [HttpGet("getAllQuizzesWithCondition")]
        public async Task<IEnumerable<ReadingQuizzes>> GetWithCondition(string language, int userProgresion)
        {

            var difficulty = 1;
            if (userProgresion >= 200)
            {
                difficulty = 2;
            }
            if (userProgresion >= 500)
            {
                difficulty = 3;
            }
            if (userProgresion >= 1000)
            {
                difficulty = 4;
            }

            return await readingQuizzes.Find(rq => rq.Language == language && rq.Difficulty == difficulty).ToListAsync();
        }

        //getQuizById
        [HttpGet("getQuizById/{id}")]
        public async Task<ActionResult<ReadingQuizzes>> GetQuizById(string id)
        {
            var readingQuiz = await readingQuizzes.Find(rq => rq.Id == id).FirstOrDefaultAsync();

            if (readingQuiz == null)
            {
                return NotFound();
            }

            return readingQuiz;
        }

        //add quizz
        [HttpPost("addQuiz")]
        public async Task<ActionResult<ReadingQuizzes>> AddQuiz(ReadingQuizzes readingQuiz)
        {
            readingQuiz.version = 0;
            await readingQuizzes.InsertOneAsync(readingQuiz);
            return readingQuiz;
        }

        //delete quizz
        [HttpDelete("deleteQuizById/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var readingQuiz = await readingQuizzes.Find(rq => rq.Id == id).FirstOrDefaultAsync();

            if (readingQuiz == null)
            {
                return NotFound();
            }

            await readingQuizzes.DeleteOneAsync(rq => rq.Id == id);

            return NoContent();
        }

        //update quizz
        [HttpPut("editQuizById/{id}")]
        public async Task<IActionResult> Update(string id, ReadingQuizzes readingQuiz)
        {
            if (id != readingQuiz.Id)
            {
                return BadRequest();
            }

            var existingReadingQuiz = await readingQuizzes.Find(rq => rq.Id == id).FirstOrDefaultAsync();

            if (existingReadingQuiz == null)
            {
                return NotFound();
            }

            readingQuiz.version = existingReadingQuiz.version + 1;

            await readingQuizzes.ReplaceOneAsync(rq => rq.Id == id, readingQuiz);

            return NoContent();
        }
    }
}
