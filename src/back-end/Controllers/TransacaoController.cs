using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiFinanceira.Models;
using ApiFinanceira.DTOs;

namespace ApiFinanceira.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TransacaoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: v1/Transacao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransacaoCriadaDto>>> GetTransacoes()
        {
            var listaTransacoes = await _context.Transacoes.Select(t => new TransacaoCriadaDto
            {
                Id = t.Id,
                Descricao = t.Descricao,
                Valor = t.Valor,
                Tipo = t.Tipo.ToString(),
                PessoaId = t.PessoaId,
                CategoriaId = t.CategoriaId
            }).ToListAsync();

            return listaTransacoes;
        }

        // GET: v1/Transacao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransacaoCriadaDto>> GetTransacao(int id)
        {
            var transacao = await _context.Transacoes.Select(t => new TransacaoCriadaDto
            {
                Id = t.Id,
                Descricao = t.Descricao,
                Valor = t.Valor,
                Tipo = t.Tipo.ToString(),
                PessoaId = t.PessoaId,
                CategoriaId = t.CategoriaId
            }).FirstOrDefaultAsync(t => t.Id == id);

            if (transacao == null)
            {
                return NotFound();
            }

            return transacao;
        }

        // POST: v1/Transacao
        [HttpPost]
        public async Task<ActionResult<Transacao>> PostTransacao(TransacaoCriarDto transacao)
        {
            var pessoa = await _context.Pessoas.FindAsync(transacao.PessoaId);
            var categoria = await _context.Categorias.FindAsync(transacao.CategoriaId);

            if (pessoa == null) return BadRequest($"Pessoa com ID {transacao.PessoaId} não encontrada.");
            if (categoria == null) return BadRequest($"Categoria com ID {transacao.CategoriaId} não encontrada.");

            if (pessoa.Idade < 18 && transacao.Tipo == (int)Tipo.Receita)
            {
                return BadRequest("Pessoas menores de 18 anos não podem registrar receitas.");
            }

            if (transacao.Tipo != (int)Tipo.Receita && transacao.Tipo != (int)Tipo.Despesa)
            {
                return BadRequest("Tipo de transação inválido. Use 1 para Receita ou 2 para Despesa.");
            }

            if (transacao.Tipo == (int)Tipo.Despesa && categoria.Finalidade == Finalidade.Receita)
            {
                return BadRequest("Não é permitido registrar uma despesa em uma categoria exclusiva de receita.");
            }

            if (transacao.Tipo == (int)Tipo.Receita && categoria.Finalidade == Finalidade.Despesa)
            {
                return BadRequest("Não é permitido registrar uma receita em uma categoria exclusiva de despesa.");
            }

            var novaTransacao = new Transacao
            {
                Descricao = transacao.Descricao,
                Valor = transacao.Valor,
                Tipo = (Tipo)transacao.Tipo,
                PessoaId = transacao.PessoaId,
                CategoriaId = transacao.CategoriaId
            };

            _context.Transacoes.Add(novaTransacao);
            await _context.SaveChangesAsync();
            
            var resposta = new TransacaoCriadaDto
            {
                Id = novaTransacao.Id,
                Descricao = novaTransacao.Descricao,
                Valor = novaTransacao.Valor,
                Tipo = novaTransacao.Tipo.ToString(),
                PessoaId = novaTransacao.PessoaId,
                CategoriaId = novaTransacao.CategoriaId
            };

            return CreatedAtAction("GetTransacao", new { id = novaTransacao.Id }, resposta);
        }
    }
}
