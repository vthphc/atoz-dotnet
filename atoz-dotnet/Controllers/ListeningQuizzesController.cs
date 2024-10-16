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

        //get all
        [HttpGet]
        public async Task<IEnumerable<ListeningQuizzes>> Get()
        {
            return await listeningQuizzes.Find(FilterDefinition<ListeningQuizzes>.Empty).ToListAsync();
        }

        //get by id
        [HttpGet("{id:length(24)}", Name = "GetListeningQuizzes")]
        public async Task<ActionResult<ListeningQuizzes>> Get(string id)
        {
            var listeningQuiz = await listeningQuizzes.Find(lq => lq.Id == id).FirstOrDefaultAsync();

            if (listeningQuiz == null)
            {
                return NotFound();
            }

            return listeningQuiz;
        }

        //add quizz
        [HttpPost]
        public async Task<ActionResult<ListeningQuizzes>> Create(ListeningQuizzes listeningQuiz)
        {
            await listeningQuizzes.InsertOneAsync(listeningQuiz);
            return CreatedAtRoute("GetListeningQuizzes", new { id = listeningQuiz.Id.ToString() }, listeningQuiz);
        }

        //delete quizz
        [HttpDelete("{id:length(24)}")]
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

        // Existing code...

        // get quizzes based on user progression
        [HttpPost("GetAllQuizzesWithCondition")]
        public async Task<ActionResult<IEnumerable<ListeningQuizzes>>> GetAllQuizzesWithCondition([FromBody] List<string> publicIds)
        {
            var filter = Builders<ListeningQuizzes>.Filter.In(lq => lq.PublicId, publicIds);
            var quizzes = await listeningQuizzes.Find(filter).ToListAsync();

            return quizzes;
        }
    }
}
