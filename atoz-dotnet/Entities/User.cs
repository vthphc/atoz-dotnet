using atoz_dotnet.Converter;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static System.Runtime.InteropServices.JavaScript.JSType;

//const userSchema = mongoose.Schema({
//  userId: {
//    type: String,
//    required: true,
//  },
//  email:
//{
//type: String,
//    required: true,
//  },
//  firstName:
//{
//type: String,
//    required: true,
//  },
//  lastName:
//{
//type: String,
//    required: true,
//  },
//  age:
//{
//type: Number,
//    required: true,
//  },
//  language:
//{
//type: String,
//    required: false,
//  },
//  progression:
//{
//type: Number,
//    required: false,
//  },
//  score:
//{
//type: Number,
//    required: false,
//  },
//  profileImage:
//{
//type: String,
//    required: true,
//  },
//  userStage:
//{
//type: [[stageSchema]],
//    required: true,
//  },
//  userType:
//{
//type: String,
//    required: false,
//  },
//  userFriends:
//{
//type: Array,
//    required: true,
//  },
//});

namespace atoz_dotnet.Entities

{
    public class User
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("userId")]
        public string? UserId { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("firstName")]
        public string? FirstName { get; set; }

        [BsonElement("lastName")]
        public string? LastName { get; set; }

        [BsonElement("age")]
        public int Age { get; set; }

        [BsonElement("language")]
        public string? Language { get; set; }

        [BsonElement("progression")]
        public int Progression { get; set; }

        [BsonElement("score")]
        public int Score { get; set; }

        [BsonElement("profileImage")]
        public string? ProfileImage { get; set; }

        [BsonElement("userStage")]
        [BsonSerializer(typeof(StageArraySerializer))]
        public required Stage[] UserStage { get; set; }

        [BsonElement("userType")]
        public string? UserType { get; set; }

        [BsonElement("userFriends")]
        public string[]? UserFriends { get; set; }
        [BsonElement("__v")]
        public int __v { get; set; }
    }

    //public class ArrayStage
    //{
    //    public Stage[] stages;
    //}
}
