﻿using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
    public interface IUPerguntasRepository
    {
        void Perguntar(string usuario, string pergunta);
     
     
    }
}
