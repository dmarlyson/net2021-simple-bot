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
        string strCon;
        public PerguntasSqlRepository(string strConnection)
        {
            strCon = strConnection;
        }      
    
        public void Perguntar(string usuario, string pergunta)
        {
            using (SqlConnection con = new SqlConnection(strCon))
            {
                con.Execute("insert into Pergunta (Usuario, Pergunta) values (@user, @perg)", 
                    new { user = usuario, perg = pergunta });
            }
        }
    }
}
