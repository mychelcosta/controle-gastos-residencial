using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiFinanceira.Models;
using ApiFinanceira.DTOs;

namespace ApiFinanceira.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PessoaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: v1/Pessoa
        [HttpGet]
        public async Task<ActionResult<PessoasRelatorioGeralDto>> GetPessoa()
        {
            var listaPessoas = await _context.Pessoas.Select(p => new PessoaRelatorioDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Idade = p.Idade,
                TotalReceitas = p.Transacoes.Where(t => t.Tipo == Tipo.Receita).Sum(t => t.Valor),
                TotalDespesas = p.Transacoes.Where(t => t.Tipo == Tipo.Despesa).Sum(t => t.Valor)
            }).ToListAsync();

            var relatorio = new PessoasRelatorioGeralDto
            {
                Pessoas = listaPessoas,
                TotalGeralReceitas = listaPessoas.Sum(p => p.TotalReceitas),
                TotalGeralDespesas = listaPessoas.Sum(p => p.TotalDespesas)
            };

            return relatorio;
        }

        // GET: v1/Pessoa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaRelatorioDto>> GetPessoa(int id)
        {
            var pessoa = await _context.Pessoas.Select(p => new PessoaRelatorioDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Idade = p.Idade,
                TotalReceitas = p.Transacoes.Where(t => t.Tipo == Tipo.Receita).Sum(t => t.Valor),
                TotalDespesas = p.Transacoes.Where(t => t.Tipo == Tipo.Despesa).Sum(t => t.Valor)
            }).FirstOrDefaultAsync(p => p.Id == id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return pessoa;
        }

        // PUT: v1/Pessoa/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa(int id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            _context.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PessoaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: v1/Pessoa
        [HttpPost]
        public async Task<ActionResult<Pessoa>> PostPessoa(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            var resposta = new PessoaCriadaDto
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Idade = pessoa.Idade
            };

            return CreatedAtAction("GetPessoa", new { id = pessoa.Id }, resposta);
        }

        // DELETE: v1/Pessoa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PessoaExists(int id)
        {
            return _context.Pessoas.Any(e => e.Id == id);
        }
    }
}
