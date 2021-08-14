using NUnit.Framework;
using MongoDB.Driver;
using SimpleBotCore.Repositories;

namespace Test
{
    public class PerguntasMongoRepositoryTest
    {
        #region Init & Setup

        private MongoClient _mongoClient;
        private PerguntasMongoRepository _repo;

        [SetUp]
        public void Setup()
        {
            _mongoClient = new MongoClient();
            _repo = new PerguntasMongoRepository(_mongoClient);
        }

        #endregion

        [Test]
        [Order(1)]
        public void Create()
        {
            _repo.Perguntar("TestUser", "teste 5");

            Assert.Pass();
        }
    }
}
