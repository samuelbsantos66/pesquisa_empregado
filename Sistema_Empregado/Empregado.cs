using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sistema_Empregado
{
    internal class Empregado
    {
        public int matricula { get; set; }
        public string cpf { get; set; }

        public string nome { get; set; }

        public string endereco { get; set; }

        public Empregado(string Cpf, string Nome, string Endereco)
        {
            cpf = Cpf;
            nome = Nome;
            endereco = Endereco;
        }

        public override string ToString()
        {
            return $"Empregado -> Matricula: {matricula} | Cpf: {cpf} | Nome: {nome} | Endereco: {endereco}  ";
        }


        private static void Salvar(Empregado empregado, SqlConnection conexao)
        {
            Console.WriteLine(" == Salvando o empregado == ");

            var Cmd = conexao.CreateCommand();
            Cmd.CommandText = "INSERT INTO EMPREGADO (nome) VALUES (@nome)";

            var param = new SqlParameter("nome", empregado.nome);
            Cmd.Parameters.Add(param);

            Cmd.ExecuteNonQuery();
        }

    } 
}
