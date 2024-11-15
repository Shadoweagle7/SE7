using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace SE7.Messaging
{
    public class Message
    {
        private readonly byte[] Bytes;

        internal Message(byte[] bytes) => Bytes = bytes;

        public static MessageBuilder Create<T>(T obj, JsonSerializer? jsonSerializer = null)
        {
            using var memoryStream = new MemoryStream();
            using var bsonDataWriter = new BsonDataWriter(memoryStream);
            
            jsonSerializer ??= new JsonSerializer();

            jsonSerializer.Serialize(bsonDataWriter, obj);

            return new MessageBuilder(memoryStream.ToArray());
        }
    }
}
