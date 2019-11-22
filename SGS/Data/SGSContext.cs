using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SGS.Models
{
    public class SGSContext : IdentityDbContext
    {
        public SGSContext (DbContextOptions<SGSContext> options)
            : base(options)
        {
        }

        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Disciplina> Disciplina { get; set; }
        public DbSet<AlunoNota> AlunoNota { get; set; }
    }
}
