using System.Collections.Generic;
using MySqlConnector;

namespace TreinandoDDLBackend.Models
{
    public class UsuarioBanco
    {
        private const string dadosConexao = "Database=treinandoDDLBackend,Data Source= localhost; User id= root";

        public void Inserir(Usuario usuario)
        {
            MySqlConnection conexao = new MySqlConnection(dadosConexao);
            conexao.Open();
            string query = "insert into Usuario(Nome, Senha)values(@Nome, @Senha)";
            MySqlCommand comando = new MySqlCommand(query,conexao);
            comando.Parameters.AddWithValue("@Nome", usuario.Nome);
            comando.Parameters.AddWithValue("@Senha", usuario.Senha);
            comando.ExecuteNonQuery();
            conexao.Close();
        }

        public List<Usuario> BuscarTodos()
        {
            MySqlConnection conexao = new MySqlConnection(dadosConexao);
            conexao.Open();
            string sql = "select * from Usuario";
            MySqlCommand comando = new MySqlCommand(sql,conexao);
            MySqlDataReader reader = comando.ExecuteReader();
            List<Usuario> lista = new List<Usuario>();
            while(reader.Read())
            {
                Usuario user = new Usuario();
                user.Id = reader.GetInt32("Id");
                if(!reader.IsDBNull(reader.GetOrdinal("Nome")))
                    user.Nome= reader.GetString("Nome");
                if(!reader.IsDBNull(reader.GetOrdinal("Senha")))
                    user.Senha = reader.GetString("Senha");
                lista.Add(user);
            }
            conexao.Close();
            return lista;  
        }

        public Usuario QueryLogin(Usuario usuario)
        {
            MySqlConnection conexao = new MySqlConnection(dadosConexao);
            conexao.Open();
            string sql = "select * from Usuario where Nome = @Nome and Senha = @Senha";
            MySqlCommand comando = new MySqlCommand(sql,conexao);
            comando.Parameters.AddWithValue("@Nome", usuario.Nome);
            comando.Parameters.AddWithValue("@Senha", usuario.Senha);
            MySqlDataReader reader = comando.ExecuteReader();
            Usuario us = null;
            if(reader.Read())
            {
                us= new Usuario();
                us.Id = reader.GetInt32("Id");
                if(!reader.IsDBNull(reader.GetOrdinal("Nome")))
                    us.Nome = reader.GetString("Nome");
                if(!reader.IsDBNull(reader.GetOrdinal("Senha")))
                    us.Senha = reader.GetString("Senha");
            }
            conexao.Close();
            return us;


        }

    }
}