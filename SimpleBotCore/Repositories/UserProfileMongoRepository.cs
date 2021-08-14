using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SimpleBotCore.Repositories
{
    public class UserProfileMongoRepository : IUserProfileRepository
    {
        #region Init

        private MongoClient _mongoClient;
        private IMongoCollection<SimpleUser> _col_user;

        public UserProfileMongoRepository(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            var db = _mongoClient.GetDatabase("db_fiap");
            var col_user = db.GetCollection<SimpleUser>("col_user");

            _col_user = col_user;
        }

        #endregion

        public bool AtualizaCor(string userId, string cor)
        {
            if (string.IsNullOrEmpty(cor))
                throw new ArgumentNullException(nameof(cor));

            var user = GetUser(userId);

            if (user == null)
                throw new InvalidOperationException("Usuário não existe");

            user.Cor = cor;

            FilterDefinition<SimpleUser> filtro = Builders<SimpleUser>.Filter.Eq(x => x.Id, user.Id);
            UpdateDefinition<SimpleUser> set = Builders<SimpleUser>.Update.Set(x => x.Cor, user.Cor);

            return UpdateUser(filtro, set);
        }

        public bool AtualizaIdade(string userId, int idade)
        {
            if (idade <= 0)
                throw new ArgumentOutOfRangeException(nameof(idade));

            var user = GetUser(userId);

            if (user == null)
                throw new InvalidOperationException("Usuário não existe");

            user.Idade = idade;

            FilterDefinition<SimpleUser> filtro = Builders<SimpleUser>.Filter.Eq(x => x.Id, user.Id);
            UpdateDefinition<SimpleUser> set = Builders<SimpleUser>.Update.Set(x => x.Idade, user.Idade);

            return UpdateUser(filtro, set);
        }

        public bool AtualizaNome(string userId, string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var user = GetUser(userId);

            if (user == null)
                throw new InvalidOperationException("Usuário não existe");

            user.Nome = name;

            FilterDefinition<SimpleUser> filtro = Builders<SimpleUser>.Filter.Eq(x => x.Id, user.Id);
            UpdateDefinition<SimpleUser> set = Builders<SimpleUser>.Update.Set(x => x.Nome, user.Nome);

            return UpdateUser(filtro, set);
        }

        public SimpleUser Create(SimpleUser user)
        {
            if (GetUser(user.Id) != null)
                throw new InvalidOperationException("Usuário ja existente");

            SaveUser(user);

            return user;
        }

        public SimpleUser TryLoadUser(string userId)
        {
            return GetUser(userId);
        }

        #region Métodos privados

        private SimpleUser GetUser(string userId)
        {
            var filtro = Builders<SimpleUser>.Filter.Eq(x => x.Id, userId);

            SimpleUser simpleUser = _col_user.Find(filtro).FirstOrDefault();

            return simpleUser;
        }

        private void SaveUser(SimpleUser user)
        {
            _col_user.InsertOne(user);
        }

        //TODO: Generalizar função
        private bool UpdateUser(FilterDefinition<SimpleUser> filtro, UpdateDefinition<SimpleUser> set)
        {
            UpdateResult result = _col_user.UpdateOne(filtro, set);

            return result.ModifiedCount > 0;
        }

        #endregion
    }
}
