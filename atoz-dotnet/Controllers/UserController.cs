using atoz_dotnet.Database;
using atoz_dotnet.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace atoz_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> users;
        public UserController(MongoDbService mongoDbService)
        {
            users = mongoDbService.GetDatabase().GetCollection<User>("users");
        }

        [HttpGet("getAllUsers")]
        public async Task<IEnumerable<User>> Get()
        {
            return await users.Find(FilterDefinition<User>.Empty).ToListAsync();
        }

        //get user by id
        [HttpGet("getAllUsers/{id}")]
        public async Task<User> Get(string id)
        {
            return await users.Find<User>(user => user.UserId == id).FirstOrDefaultAsync();
        }

        //get user by email
        [HttpGet("getUserByEmail/{email}")]
        public async Task<User> GetByEmail(string email)
        {
            return await users.Find<User>(user => user.Email == email).FirstOrDefaultAsync();
        }

        //add user
        [HttpPost("addUser")]
        public async Task<IActionResult> Post(User user)
        {
            user.__v = 0;
            await users.InsertOneAsync(user);
            return CreatedAtAction(nameof(Get), new { id = user.UserId }, user);
        }

        //delete user by email
        [HttpDelete("deleteUserByEmail/{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            var user = await users.Find<User>(user => user.Email == email).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            await users.DeleteOneAsync(user => user.Email == email);
            return NoContent();
        }

        //delete user by id
        [HttpDelete("deleteUserById/{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            var user = await users.Find<User>(user => user.UserId == id).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            await users.DeleteOneAsync(user => user.UserId == id);
            return NoContent();
        }

        //update user
        [HttpPut("editUserById/{id}")]
        public async Task<IActionResult> Update(string id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            var existingUser = await users.Find<User>(user => user.UserId == id).FirstOrDefaultAsync();

            if (existingUser == null)
            {
                return NotFound();
            }

            await users.ReplaceOneAsync(user => user.UserId == id, user);
            return NoContent();
        }

        //add friend
        [HttpPut("addFriend/{userId}/{friendId}")]
        public async Task<IActionResult> AddFriend(string userId, string friendId)
        {
            var user = await users.Find(user => user.UserId == userId).FirstOrDefaultAsync();
            var friend = await users.Find(user => user.UserId == friendId).FirstOrDefaultAsync();

            if (user == null || friend == null)
            {
                return NotFound();
            }

            var updateUser = Builders<User>.Update.AddToSet(u => u.UserFriends, friendId);
            var updateFriend = Builders<User>.Update.AddToSet(u => u.UserFriends, userId);

            var userResult = await users.UpdateOneAsync(user => user.UserId == userId, updateUser);
            var friendResult = await users.UpdateOneAsync(user => user.UserId == friendId, updateFriend);

            if (!userResult.IsAcknowledged || !friendResult.IsAcknowledged)
            {
                return StatusCode(500, "Failed to update friend lists");
            }

            return NoContent();
        }

        //remove friend
        [HttpPut("removeFriend/{userId}/{friendId}")]
        public async Task<IActionResult> RemoveFriend(string userId, string friendId)
        {
            var user = await users.Find<User>(user => user.UserId == userId).FirstOrDefaultAsync();
            var friend = await users.Find<User>(user => user.UserId == friendId).FirstOrDefaultAsync();

            if (user == null || friend == null)
            {
                return NotFound();
            }

            user.UserFriends = user.UserFriends?.Where(f => f != friendId).ToArray();
            friend.UserFriends = friend.UserFriends?.Where(f => f != userId).ToArray();

            await users.ReplaceOneAsync(user => user.UserId == userId, user);
            await users.ReplaceOneAsync(user => user.UserId == friendId, friend);

            return NoContent();
        }
    }
}
