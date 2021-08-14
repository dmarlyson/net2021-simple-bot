using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data.SqlClient;
using Dapper;

namespace SimpleBotCore.Repositories
{
    public class PerguntasSqlRepository : IUPerguntasRepository
    {
        private string _sqlStrConnection;

        public PerguntasSqlRepository(string sqlStrConnection)
        {
            _sqlStrConnection = sqlStrConnection;
        }      
    
        public void Perguntar(string usuario, string pergunta)
        {
            using (SqlConnection con = new SqlConnection(_sqlStrConnection))
            {
                con.Execute("INSERT INTO Pergunta (Usuario, Pergunta) VALUES (@user, @perg)",
                        new { user = usuario, perg = pergunta });
            }
        }
    }
}
