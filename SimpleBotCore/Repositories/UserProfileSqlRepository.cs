using SimpleBotCore.Logic;
using System;
using System.Data.SqlClient;
using Dapper;

namespace SimpleBotCore.Repositories
{
    public class UserProfileSqlRepository : IUserProfileRepository
    {
        private string _sqlStrConnection;

        public UserProfileSqlRepository(string sqlStrConnection)
        {
            _sqlStrConnection = sqlStrConnection;
        }

        public bool AtualizaCor(string userId, string cor)
        {
            if (string.IsNullOrEmpty(cor))
                throw new ArgumentNullException(nameof(cor));

            var user = GetUser(userId);

            if (user == null)
                throw new InvalidOperationException("Usuário não existe");

            user.Cor = cor;

            return UpdateUser(user);
        }

        public bool AtualizaIdade(string userId, int idade)
        {
            if (idade <= 0)
                throw new ArgumentOutOfRangeException(nameof(idade));

            var user = GetUser(userId);

            if (user == null)
                throw new InvalidOperationException("Usuário não existe");

            user.Idade = idade;

            return UpdateUser(user);
        }

        public bool AtualizaNome(string userId, string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var user = GetUser(userId);

            if (user == null)
                throw new InvalidOperationException("Usuário não existe");

            user.Nome = name;

            return UpdateUser(user);
        }

        public SimpleUser Create(SimpleUser user)
        {
            using (SqlConnection con = new SqlConnection(_sqlStrConnection))
            {
                user.Id = con.QuerySingle<string>("INSERT INTO Usuario (Nome, Idade, Cor) OUTPUT INSERTED.Id VALUES (@nome, @idade, @cor)",
                            new { nome = user.Nome, idade = user.Idade, cor = user.Cor });
            }

            return user;
        }

        public SimpleUser TryLoadUser(string userId)
        {
            SimpleUser simpleUser = GetUser(userId);

            return simpleUser;
        }

        #region Métodos privados

        private SimpleUser GetUser(string userId)
        {
            SimpleUser simpleUser = null;

            using (SqlConnection con = new SqlConnection(_sqlStrConnection))
            {
                var obj = con.QuerySingle("SELECT * FROM Usuario WHERE Id = @Id",
                            new { Id = Convert.ToInt32(userId) });

                string id = Convert.ToString(obj.Id);
                simpleUser = new SimpleUser(id);
                simpleUser.Nome = obj.Nome;
                simpleUser.Idade = obj.Idade;
                simpleUser.Cor = obj.Cor;
            }

            return simpleUser;
        }

        private bool UpdateUser(SimpleUser simpleUser)
        {
            int affectedRows = 0;

            using (SqlConnection con = new SqlConnection(_sqlStrConnection))
            {
                affectedRows = con.Execute("UPDATE Usuario SET Nome = @Nome, Idade = @Idade, Cor = @Cor WHERE Id = @Id",
                    new { Id = simpleUser.Id, Nome = simpleUser.Nome, Idade = simpleUser.Idade, Cor = simpleUser.Cor });
            }

            return affectedRows > 0;
        }

        #endregion
    }
}
