using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Application.Dados.Repositorios
{
    public interface IRepositorioBase<TContexto, TEntidade>
        where TContexto : DbContext
        where TEntidade : class
    {
        TEntidade Adicionar(TEntidade entidade, int quantidade);
        TEntidade Atualizar(TEntidade entidade);
        void Remover(TEntidade entidade);
        TEntidade ObterPorId(int id);
        List<TEntidade> ListarTodos();

        Task<TEntidade> AdicionarAssincrono(TEntidade entidade, int quantidade);
        Task<TEntidade> AtualizarAssincrono(TEntidade entidade);
        Task RemoverAssincrono(TEntidade entidade);
        Task<TEntidade> ObterPorIdAssincrono(int id);
        Task<List<TEntidade>> ListarTodosAssincrono();
    }
}