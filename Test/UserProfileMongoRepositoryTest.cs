using NUnit.Framework;
using SimpleBotCore.Logic;
using SimpleBotCore.Repositories;
using System;
using MongoDB.Driver;

namespace Test
{
    public class UserProfileMongoRepositoryTest
    {
        #region Init & Setup

        private MongoClient _mongoClient;
        private UserProfileMongoRepository _repo;
        private SimpleUser _fakeUser;
        private string _userId = Guid.NewGuid().ToString();

        [SetUp]
        public void Setup()
        {
            _mongoClient = new MongoClient();
            _repo = new UserProfileMongoRepository(_mongoClient);

            _fakeUser = new SimpleUser(_userId);
            _fakeUser.Nome = "Maurício";
            _fakeUser.Cor = "Azul";
            _fakeUser.Idade = 30;
        }

        #endregion

        [Test]
        [Order(1)]
        public void Create()
        {
            SimpleUser simpleUser = _repo.Create(_fakeUser);

            Assert.IsNotNull(simpleUser);
        }

        [Test]
        [Order(2)]
        public void TryLoadUser()
        {
            SimpleUser simpleUser = _repo.TryLoadUser(_fakeUser.Id);

            Assert.IsNotNull(simpleUser);
        }

        [Test]
        [Order(3)]
        public void AtualizaCor()
        {
            bool atualizou = _repo.AtualizaCor(_fakeUser.Id, "Verde");

            Assert.IsTrue(atualizou);
        }

        [Test]
        [Order(4)]
        public void AtualizaIdade()
        {
            bool atualizou = _repo.AtualizaIdade(_fakeUser.Id, 32);

            Assert.IsTrue(atualizou);
        }

        [Test]
        [Order(5)]
        public void AtualizaNome()
        {
            bool atualizou = _repo.AtualizaNome(_fakeUser.Id, "Mauricio SG");

            Assert.IsTrue(atualizou);
        }
    }
}