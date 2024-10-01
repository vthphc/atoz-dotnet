using atoz_dotnet.Database;
using atoz_dotnet.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace atoz_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMongoCollection<Book>? books;

        public BookController(MongoDbService mongoDbService)
        {
            books = mongoDbService.GetDatabase().GetCollection<Book>("books");
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> Get()
        {
            return await books.Find(FilterDefinition<Book>.Empty).ToListAsync();
        }
    }
}
