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
    public class ListeningQuizzesController : ControllerBase
    {
        private readonly IMongoCollection<ListeningQuizzes>? listeningQuizzes;

        public ListeningQuizzesController(MongoDbService mongoDbService)
        {
            listeningQuizzes = mongoDbService.GetDatabase().GetCollection<ListeningQuizzes>("listeningquizzes");
        }

        //get all
        [HttpGet("getAllQuizzes")]
        public async Task<IEnumerable<ListeningQuizzes>> GetAllQuizzes()
        {
            return await listeningQuizzes.Find(FilterDefinition<ListeningQuizzes>.Empty).ToListAsync();
        }

        //get by id
        [HttpGet("getQuizById/{id}")]
        public async Task<ActionResult<ListeningQuizzes>> GetQuizById(string id)
        {
            var listeningQuiz = await listeningQuizzes.Find(lq => lq.Id == id).FirstOrDefaultAsync();

            if (listeningQuiz == null)
            {
                return NotFound();
            }

            return listeningQuiz;
        }

        //add quizz
        [HttpPost("addQuiz")]
        public async Task<ActionResult<ListeningQuizzes>> AddQuiz(ListeningQuizzes listeningQuiz)
        {
            listeningQuiz.version = 0;
            await listeningQuizzes.InsertOneAsync(listeningQuiz);
            return listeningQuiz;
        }

        //delete quizz
        [HttpDelete("deleteQuizById/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var listeningQuiz = await listeningQuizzes.Find(lq => lq.Id == id).FirstOrDefaultAsync();

            if (listeningQuiz == null)
            {
                return NotFound();
            }

            await listeningQuizzes.DeleteOneAsync(lq => lq.Id == id);

            return NoContent();
        }

        // get quizzes based on user progression
        //[HttpPost("getAllQuizzesWithCondition")]
        //public async Task<IEnumerable<ListeningQuizzes>> GetAllQuizzesWithCondition(int userProgression, string language)
        //{
        //    var difficulty = 1;
        //    if (userProgression >= 200)
        //    {
        //        difficulty = 2;
        //    }
        //    if (userProgression >= 500)
        //    {
        //        difficulty = 3;
        //    }
        //    if (userProgression >= 1000)
        //    {
        //        difficulty = 4;
        //    }
        //    return await listeningQuizzes.Find(lq => lq.Language == language && lq.Difficulty == difficulty).ToListAsync();
        //}
    }
}
