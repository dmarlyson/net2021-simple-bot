﻿using SimpleBotCore.Logic;
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

            var db = this.client.GetDatabase("dbFIAP");

            var col01 = db.GetCollection<BsonDocument>("col01");
            this._collection = col01;
        }

      

        //public void InserirPergunta(string pergunta)
        //{
        //    string connStr = "mongodb://localhost:27017";
        //    connection = new MongoClient(connStr);

        //    var db = connection.GetDatabase("dbFIAP");

        //    var col01 = db.GetCollection<BsonDocument>("col01");

        //    //var doc = BsonDocument.Parse("{nome: 'Fabriricio'}");

        //    var doc = new BsonDocument()
        //    {
        //        {"Pergunta",pergunta }

        //    };

        //    col01.InsertOne(doc);

        //}

        public void Perguntar(string usuario, string pergunta)
        {
            var doc = new BsonDocument()
            {
                {"usuario",usuario },
                { "pergunta" , pergunta}

            };

            _collection.InsertOne(doc);
        }
    }
}