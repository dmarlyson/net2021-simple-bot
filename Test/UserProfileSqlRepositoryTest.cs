using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SimpleBotCore.Logic;
using SimpleBotCore.Repositories;
using System;
using System.Data.SqlClient;

namespace Test
{
    public class UserProfileSqlRepositoryTest
    {
        #region Init & Setup

        private UserProfileSqlRepository _repo;
        private SimpleUser _fakeUser;
        private string _userId;

        [SetUp]
        public void Setup()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddUserSecrets<PerguntasSqlRepositoryTest>()
                .AddEnvironmentVariables()
                .Build();

            string sqlStrConnection = configuration["ConnectionStrings:DefaultConnection"];

            _repo = new UserProfileSqlRepository(sqlStrConnection);

            _fakeUser = new SimpleUser("");
            _fakeUser.Nome = "Maurício";
            _fakeUser.Cor = "Azul";
            _fakeUser.Idade = 30;
        }

        #endregion

        [Test]
        [Order(1)]
        public void Create()
        {
            _fakeUser = _repo.Create(_fakeUser);

            _userId = _fakeUser.Id;

            Assert.IsNotEmpty(_fakeUser.Id);
        }

        [Test]
        [Order(2)]
        public void TryLoadUser()
        {
            SimpleUser simpleUser = _repo.TryLoadUser(_userId);

            Assert.IsNotNull(simpleUser);
        }

        [Test]
        [Order(3)]
        public void AtualizaCor()
        {
            bool atualizou = _repo.AtualizaCor(_userId, "Verde");

            Assert.IsTrue(atualizou);
        }

        [Test]
        [Order(4)]
        public void AtualizaIdade()
        {
            bool atualizou = _repo.AtualizaIdade(_userId, 32);

            Assert.IsTrue(atualizou);
        }

        [Test]
        [Order(5)]
        public void AtualizaNome()
        {
            bool atualizou = _repo.AtualizaNome(_userId, "Mauricio SG");

            Assert.IsTrue(atualizou);
        }
    }
}