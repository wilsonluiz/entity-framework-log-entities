using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Dados;
using Application.Dados.Entidades;
using Application.Dados.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositorioBase<HrContext, Employee> _repo;
        private readonly IRepositorioBase<HrSimuladoContext, Employee> _repoSimulado;
        private readonly IUnidadeDeTrabalho<HrContext> _unidadeDeTrabalho;
        private const string NomeArquivo = "employees.json";

        public EmployeesController(IRepositorioBase<HrContext, Employee> repo,
            IRepositorioBase<HrSimuladoContext, Employee> repoSimulado,
            IUnidadeDeTrabalho<HrContext> unidadeDeTrabalho)
        {
            _repo = repo;
            _repoSimulado = repoSimulado;
            _unidadeDeTrabalho = unidadeDeTrabalho;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterFuncionarioPorId([FromRoute] int id)
        {
            var funcionario = await _repo.ObterPorIdAssincrono(id);
            if (funcionario == null)
                return NotFound($"Não foi encontrado empregado #{id}");

            return Ok(funcionario);
        }

        [HttpGet]
        public async Task<IActionResult> ListarFuncionarios()
        {
            var funcionarios = await _repo.ListarTodosAssincrono();

            var diretorioBase = AppDomain.CurrentDomain.BaseDirectory;
            
            var caminhoArquivo = Path.Combine(diretorioBase, NomeArquivo);

            List<Employee> json;
            using (var reader = new StreamReader(caminhoArquivo))
            {
                var dados = reader.ReadToEnd();
                json = JsonConvert.DeserializeObject<List<Employee>>(dados);
            }

            return Ok(json);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarFuncionario([FromBody] Employee novoFuncionario, [FromQuery] int quantidade = 10)
        {
            var funcionario = await _repo.AdicionarAssincrono(novoFuncionario, quantidade);

            var repo = (RepositorioBase<HrContext, Employee>) _repo;
            EnivarEntidadeParaArquivo(repo);

            _unidadeDeTrabalho.Finalizar();

            return Created("", funcionario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarFuncionario([FromRoute] int id,
            [FromBody] Employee funcionarioAtualizado)
        {
            if (id != funcionarioAtualizado.EmployeeId)
                return Conflict();

            var funcionario = await _repo.AtualizarAssincrono(funcionarioAtualizado);

            return Ok(funcionario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirFuncionario([FromRoute] int id)
        {
            var funcionario = await _repo.ObterPorIdAssincrono(id);
            if (funcionario == null)
                return NotFound($"Funcionário #{id} não encontrado");

            await _repo.RemoverAssincrono(funcionario);

            return NoContent();
        }

        private void EnivarEntidadeParaArquivo(RepositorioBase<HrContext, Employee> repo)
        {
            var recipiente = repo.Recipente;
            var employees = JsonConvert.SerializeObject(recipiente.Employees);
            var diretorioBase = AppDomain.CurrentDomain.BaseDirectory;
            var caminhoArquivo = Path.Combine(diretorioBase, NomeArquivo);

            if (System.IO.File.Exists(caminhoArquivo))
                System.IO.File.Delete(caminhoArquivo);

            using (var writer = System.IO.File.AppendText(caminhoArquivo))
            {
                writer.WriteLine(employees);
            }
        }
    }
}