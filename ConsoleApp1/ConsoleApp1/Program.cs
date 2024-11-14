//using Microsoft.Data.SqlClient;

using Microsoft.Data.SqlClient;

namespace ConsoleApp1
{
    internal class Program
    {

        private static void Salvar(Autor autor, SqlConnection conexao)
        {
            Console.WriteLine(" == Salvando o Autor == ");

            var Cmd = conexao.CreateCommand();
            Cmd.CommandText = "INSERT INTO AUTOR (Nome) VALUES (@nome)";

            var param = new SqlParameter("nome", autor.Nome);
            Cmd.Parameters.Add(param);

            Cmd.ExecuteNonQuery();
        }

        private static void Salvar(Livro livro, SqlConnection conexao)
        {
            Console.WriteLine(" == Salvando o Livro == ");

            var Cmd = conexao.CreateCommand();
            Cmd.CommandText = "INSERT INTO LIVRO (Titulo) VALUES (@titulo)";

            var param = new SqlParameter("titulo", livro.Titulo);
            Cmd.Parameters.Add(param);

            Cmd.ExecuteNonQuery();

            AtualizarTableAutorComIdLivro(livro.AutorDoLivro, conexao);
        }

        private static void AtualizarTableAutorComIdLivro(Autor autor, SqlConnection conexao)
        {
            var Cmd = conexao.CreateCommand();
            Cmd.CommandText = "SELECT MAX(Id) from Livro";
            var resultado = Cmd.ExecuteReader();

            int idLivroRecuperado = 0;
            if (resultado.Read())
                idLivroRecuperado = resultado.GetInt32(0);
            
            resultado.Close();


            Cmd.CommandText = "UPDATE AUTOR SET Livro_idLivro = @idMax WHERE Id = @IdAutor";
            Cmd.Parameters.Add(new SqlParameter("idMax", idLivroRecuperado));
            Cmd.Parameters.Add(new SqlParameter("IdAutor", autor.Id));

            Cmd.ExecuteNonQuery();
        }

        static void Main(string[] args)
        {
            SqlConnection? conexao = null;

            string URL = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DBAutorLivro1x1;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            try
            {
                conexao = new(URL);
                conexao.Open();
                Console.WriteLine("OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine(" NOT OK");
            }

            if (conexao != null)
            {
                Autor autor = new("Bruno");
                //Salvar(autor, conexao);

                autor.Id = 1; 

                Livro livro = new("POO Maneiro BD", autor);
                Salvar(livro, conexao);

            }
            conexao?.Close();
        }
    }
}
