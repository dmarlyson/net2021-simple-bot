using System;

namespace SimpleBotCore.Logic
{
    public class SimpleUser
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string Cor { get; set; }

        public SimpleUser(string userId)
        {
            this.Id = userId ?? throw new ArgumentNullException(nameof(userId));
        }
    }
}
