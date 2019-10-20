using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGS.Models
{
    public class Aluno
    {
        public int AlunoId { get; set; }
        public string Cpf { get; set; }
        public string NomeDoAluno { get; set; }
        public string Endereco { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public int Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        //public Matricula Matricula { get; set; }
        //public Cursos Cursos { get; set; }

        public Aluno()
        {
        }

        public Aluno(string cpf, string nomeDoAluno, string endereco, string estado, string municipio, int telefone, string email, string senha)
        {
            Cpf = cpf;
            NomeDoAluno = nomeDoAluno;
            Endereco = endereco;
            Estado = estado;
            Municipio = municipio;
            Telefone = telefone;
            Email = email;
            Senha = senha;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
