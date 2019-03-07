using System;
using System.IO;
using System.Threading.Tasks;
using Application.Dados;
using Application.Dados.Entidades;
using Application.Dados.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/arquivos")]
    public class ArquivosController : ControllerBase
    {
        private const string NomeArquivo = "employees.json";
        private readonly IRepositorioBase<HrContext, Arquivo> _repo;

        public ArquivosController(IRepositorioBase<HrContext, Arquivo> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> ListarArquivos()
        {
            var arquivos = await _repo.ListarTodosAssincrono();

            return Ok(arquivos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterArquivorId(string id)
        {
            var arquivo = await _repo.ObterPorIdAssincrono(id);

            return Ok(arquivo);
        }

        [HttpPost]
        public async Task<IActionResult> PesistirArquivos()
        {
            var diretorioBase = AppDomain.CurrentDomain.BaseDirectory;
            var caminho = Path.Combine(diretorioBase, NomeArquivo);

            using (var reader = new StreamReader(caminho))
            {
                var conteudo = reader.ReadToEnd();
                var arquivo = new Arquivo
                {
                    Id = Guid.NewGuid().ToString(),
                    DataCriacao = DateTime.Now,
                    Dados = conteudo
                };

                await _repo.AdicionarAssincrono(arquivo);

                return Created("", arquivo);
            }
        }
    }
}