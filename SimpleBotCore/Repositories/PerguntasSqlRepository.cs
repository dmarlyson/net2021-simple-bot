using SimpleBotCore.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Data.SqlClient;

namespace SimpleBotCore.Repositories
{
    public class PerguntasSqlRepository : IUPerguntasRepository
    {
        string connection;
        public PerguntasSqlRepository(string strConnection)
        {
            connection = strConnection;
        }
      
    
        public void Perguntar(string usuario, string pergunta)
        {
            using (var con = new SqlConnection(connection))
            {
                con.Open();

                SqlCommand comando = new SqlCommand("insert into Pergunta (Usuario, Pergunta) values (@user, @perg)", con);
                comando.Parameters.AddWithValue("@user", usuario);
                comando.Parameters.AddWithValue("@perg", pergunta);

                comando.ExecuteNonQuery();
            }
        }
    }
}
