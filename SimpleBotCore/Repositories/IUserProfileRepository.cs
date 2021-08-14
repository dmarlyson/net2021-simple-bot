using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public interface IUserProfileRepository
    {
        SimpleUser TryLoadUser(string userId);

        SimpleUser Create(SimpleUser user);

        bool AtualizaNome(string userId, string name);
        bool AtualizaIdade(string userId, int idade);
        bool AtualizaCor(string userId, string cor);
    }
}
