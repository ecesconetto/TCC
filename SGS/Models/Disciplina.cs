using System.Collections.Generic;

namespace SGS.Models
{
    public class Disciplina
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual List<AlunoNota> AlunoNotas { get; set; }
    }
}