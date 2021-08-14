using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBotCore.Repositories
{
    public class PerguntasMongoRepository : IUPerguntasRepository
    {
        public MongoClient client;
        IMongoCollection<BsonDocument> _collection;
        public PerguntasMongoRepository(MongoClient client)
        {
            this.client = client;

            var db = this.client.GetDatabase("db_fiap");

            var col01 = db.GetCollection<BsonDocument>("col_pergunta");
            this._collection = col01;
        }

        public void Perguntar(string usuario, string pergunta)
        {
            var doc = new BsonDocument()
            {
                { "usuario", usuario },
                { "pergunta" , pergunta }
            };

            _collection.InsertOne(doc);
        }
    }
}
