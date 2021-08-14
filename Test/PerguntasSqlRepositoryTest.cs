using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SimpleBotCore.Repositories;
using System.Data.SqlClient;

namespace Test
{
    class PerguntasSqlRepositoryTest
    {
        #region Init & Setup

        private PerguntasSqlRepository _repo;

        [SetUp]
        public void Setup()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddUserSecrets<PerguntasSqlRepositoryTest>()
                .AddEnvironmentVariables()
                .Build();

            string sqlStrConnection = configuration["ConnectionStrings:DefaultConnection"];

            _repo = new PerguntasSqlRepository(sqlStrConnection);
        }

        #endregion

        [Test]
        [Order(1)]
        public void Perguntar()
        {
            _repo.Perguntar("TestUser", "teste 5");

            Assert.Pass();
        }
    }
}
