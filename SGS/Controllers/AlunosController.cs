using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGS.Models;
using SGS.ViewModel;

namespace SGS.Controllers
{
    public class AlunosController : Controller
    {
        private readonly SGSContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AlunosController(SGSContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Alunos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aluno.ToListAsync());
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno.FirstOrDefaultAsync(m => m.AlunoId == id);

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // GET: Alunos/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alunos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AlunoId,Cpf,NomeDoAluno,Endereco,Estado,Municipio,Telefone,Email,Senha")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aluno);
                await _context.SaveChangesAsync();

                //Cria usuário no Identity
                var usuarioCriado = Task.Run(() => _userManager.CreateAsync(
                   new IdentityUser()
                   {
                       UserName = aluno.Email,
                       NormalizedUserName = aluno.Email.ToUpper(),
                       Email = aluno.Email
                   }, aluno.Senha)).Result;

                //Busca o usuário recém criado para vincular as claims
                var newUser = await _userManager.FindByEmailAsync(aluno.Email);

                //Adiciona as claims de aluno
                var claimsAdicionadas = Task.Run(() => _userManager.AddClaimAsync(newUser, new Claim("Aluno", "Aluno"))).Result;

                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlunoId,Cpf,NomeDoAluno,Endereco,Estado,Municipio,Telefone,Email,Senha")] Aluno aluno)
        {
            if (id != aluno.AlunoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aluno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoExists(aluno.AlunoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }

        // GET: Alunoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Aluno
                .FirstOrDefaultAsync(m => m.AlunoId == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        // POST: Alunoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Aluno.FindAsync(id);
            _context.Aluno.Remove(aluno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoExists(int id)
        {
            return _context.Aluno.Any(e => e.AlunoId == id);
        }

        // GET: Notas/Aluno
        [HttpGet]
        public async Task<IActionResult> Notas()
        {
            try
            {
                var usernameLogado = _userManager.GetUserName(User);

                var model = _context.AlunoNota
                                .Include(x => x.Aluno)
                                .Include(x => x.Disciplina)
                                .Where(x => x.Aluno.Email.Equals(usernameLogado)).ToList();

                var retorno = new List<AlunoNotaViewModel>();

                foreach (var item in model)
                {
                    retorno.Add(new AlunoNotaViewModel()
                    {
                        NomeAluno = item.Aluno.NomeDoAluno,
                        NomeDisciplina = item.Disciplina.Nome,
                        Nota1 = item.Nota1,
                        Nota2 = item.Nota2,
                        Nota3 = item.Nota3,
                        Media = GetMedia(item.Nota1, item.Nota2, item.Nota3),
                        Status = GetStatus(GetMedia(item.Nota1, item.Nota2, item.Nota3)),
                        Classe = GetClass(GetMedia(item.Nota1, item.Nota2, item.Nota3))
                    });
                }

                return View(retorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private decimal GetMedia(decimal nota1, decimal nota2, decimal nota3) => (nota1 + nota2 + nota3) / 3;

        private string GetStatus(decimal media) => (media >= 7 ? "Aprovado" : "Reprovado");

        private string GetClass(decimal media) => (media >= 7 ? "aprovado" : "reprovado");
    }
}