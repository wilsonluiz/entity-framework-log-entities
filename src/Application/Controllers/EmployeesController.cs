using System.Threading.Tasks;
using Application.Dados;
using Application.Dados.Entidades;
using Application.Dados.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositorioBase<HrContext, Employee> _repo;

        public EmployeesController(IRepositorioBase<HrContext, Employee> repo)
        {
            _repo = repo;
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

            return Ok(funcionarios);
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarFuncionario([FromBody] Employee novoFuncionario)
        {
            var funcionario = await _repo.AdicionarAssincrono(novoFuncionario);

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
    }
}