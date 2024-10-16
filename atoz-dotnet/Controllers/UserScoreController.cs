using atoz_dotnet.Database;
using atoz_dotnet.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace atoz_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserScoreController : ControllerBase
    {
        private readonly IMongoCollection<UserScore> userscores;
        public UserScoreController(MongoDbService mongoDbService)
        {
            userscores = mongoDbService.GetDatabase().GetCollection<UserScore>("userscores");
        }

        [HttpGet("getAllUsersScore")]
        public async Task<IEnumerable<UserScore>> GetAllUsersScore()
        {
            return await userscores.Find(FilterDefinition<UserScore>.Empty).ToListAsync();
        }

        //get by userscore's id
        [HttpGet("getUserScoreById/{id}")]
        public async Task<UserScore> GetUserScoreById(string id)
        {
            return await userscores.Find<UserScore>(userScore => userScore.Id == id).FirstOrDefaultAsync();
        }

        //get user score by user's id
        [HttpGet("getAllUserScoreByUserId/{userId}")]
        public async Task<IEnumerable<UserScore>> GetAllUserScoreByUserId(string userId)
        {
            return await userscores.Find<UserScore>(userScore => userScore.UserId == userId).ToListAsync();
        }

        //add user score
        [HttpPost("addUserScore")]
        public async Task<UserScore> AddUserScore(UserScore userScore)
        {
            userScore.Date = DateTime.Now;
            userScore.Version = 0;
            await userscores.InsertOneAsync(userScore);
            return userScore;
        }

        //delete user score by id
        [HttpDelete("deleteUserScoreById/{id}")]
        public async Task<IActionResult> DeleteUserScoreById(string id)
        {
            var result = await userscores.DeleteOneAsync(userScore => userScore.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return Ok();
        }
        // edit user by id
        [HttpPut("editUserScoreById/{id}")]
        public async Task<IActionResult> EditUserScoreById(string id, UserScore userScore)
        {
            var result = await userscores.ReplaceOneAsync(userScore => userScore.Id == id, userScore);
            if (result.ModifiedCount == 0)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
