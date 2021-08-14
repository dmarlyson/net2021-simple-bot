using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public class UserProfileMockRepository : IUserProfileRepository
    {
        Dictionary<string, SimpleUser> _users = new Dictionary<string, SimpleUser>();

        public SimpleUser TryLoadUser(string userId)
        {
            if( Exists(userId) )
            {
                return GetUser(userId);
            }

            return null;
        }

        public SimpleUser Create(SimpleUser user)
        {
            if ( Exists(user.Id) )
                throw new InvalidOperationException("Usuário ja existente");

            SaveUser(user);

            return user;
        }

        public bool AtualizaNome(string userId, string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            var user = GetUser(userId);

            user.Nome = name;

            return SaveUser(user);
        }

        public bool AtualizaIdade(string userId, int idade)
        {
            if (idade <= 0)
                throw new ArgumentOutOfRangeException(nameof(idade));

            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            var user = GetUser(userId);

            user.Idade = idade;

            return SaveUser(user);
        }

        public bool AtualizaCor(string userId, string cor)
        {
            if (cor == null)
                throw new ArgumentNullException(nameof(cor));

            if (!Exists(userId))
                throw new InvalidOperationException("Usuário não existe");

            var user = GetUser(userId);

            user.Cor = cor;

            return SaveUser(user);
        }

        private bool Exists(string userId)
        {
            return _users.ContainsKey(userId);
        }

        private SimpleUser GetUser(string userId)
        {
            return _users[userId];
        }

        private bool SaveUser(SimpleUser user)
        {
            _users[user.Id] = user;

            return true;
        }
    }
}

