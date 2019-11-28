namespace SGS.Models
{
    public class AlunoNota
    {
        public int Id { get; set; }
        public int DisciplinaId { get; set; }
        public Disciplina Disciplina { get; set; }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public decimal Nota1 { get; set; }
        public decimal Nota2 { get; set; }  
        public decimal Nota3 { get; set; }
    }
}
