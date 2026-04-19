using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiFinanceira.Models;
using ApiFinanceira.DTOs;

namespace ApiFinanceira.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: v1/Categoria
        [HttpGet]
        public async Task<ActionResult<CategoriaRelatorioGeralDto>> GetCategorias()
        {
            //return await _context.Categorias.ToListAsync();
            var listaCategorias = await _context.Categorias.Select(c => new CategoriaRelatorioDto
            {
                Id = c.Id,
                Descricao = c.Descricao,
                Finalidade = c.Finalidade.ToString(),
                TotalReceitas = c.Transacoes.Where(t => t.Tipo == Tipo.Receita).Sum(t => t.Valor),
                TotalDespesas = c.Transacoes.Where(t => t.Tipo == Tipo.Despesa).Sum(t => t.Valor)
            }).ToListAsync();

            var relatorio = new CategoriaRelatorioGeralDto
            {
                Categorias = listaCategorias,
                TotalGeralReceitas = listaCategorias.Sum(c => c.TotalReceitas),
                TotalGeralDespesas = listaCategorias.Sum(c => c.TotalDespesas)
            };

            return relatorio;
        }

        // GET: v1/Categoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaRelatorioDto>> GetCategoria(int id)
        {
            var categoria = await _context.Categorias.Select(c => new CategoriaRelatorioDto
            {
                Id = c.Id,
                Descricao = c.Descricao,
                Finalidade = c.Finalidade.ToString(),
                TotalReceitas = c.Transacoes.Where(t => t.Tipo == Tipo.Receita).Sum(t => t.Valor),
                TotalDespesas = c.Transacoes.Where(t => t.Tipo == Tipo.Despesa).Sum(t => t.Valor)
            }).FirstOrDefaultAsync(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // POST: v1/Categoria
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(CategoriaCriarDto categoria)
        {

            if (categoria.Finalidade < 1 || categoria.Finalidade > 3)
            {
                return BadRequest("Finalidade de categoria inválida. Use 1 para Receita, 2 para Despesa ou 3 para Ambas.");
            }

            var novaCategoria = new Categoria
            {
                Descricao = categoria.Descricao,
                Finalidade = (Finalidade)categoria.Finalidade
            };

            _context.Categorias.Add(novaCategoria);
            await _context.SaveChangesAsync();

            var resposta = new CategoriaCriadaDto
            {
                Id = novaCategoria.Id,
                Descricao = novaCategoria.Descricao,
                Finalidade = novaCategoria.Finalidade.ToString()
            };

            return CreatedAtAction("GetCategoria", new { id = novaCategoria.Id }, resposta);
        }
    }
}
