using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public DbSet<SGS.Models.Aluno> Aluno { get; set; }
    }
}
