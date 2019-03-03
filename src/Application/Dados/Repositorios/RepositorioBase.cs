using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Application.Auxiliares;
using Application.Dados.Entidades;

namespace Application.Dados.Repositorios
{
    public class RepositorioBase<TContexto, TEntidade> : IRepositorioBase<TContexto, TEntidade> 
        where TContexto : DbContext
        where TEntidade : class
    {
        protected readonly TContexto Contexto;
        protected readonly Recipiente Recipente;

        public RepositorioBase(TContexto contexto)
        {
            Contexto = contexto;
            Recipente = new Recipiente();
        }

        public TEntidade Adicionar(TEntidade entidade)
        {
            Contexto.Set<TEntidade>().Add(entidade);
            Contexto.SaveChanges();

            ProcessarEntidade(entidade);

            return entidade;
        }

        public TEntidade Atualizar(TEntidade entidade)
        {
            Contexto.Set<TEntidade>().Attach(entidade);
            Contexto.Entry(entidade).State = EntityState.Modified;
            Contexto.SaveChanges();

            return entidade;
        }

        public void Remover(TEntidade entidade)
        {
            Contexto.Set<TEntidade>().Remove(entidade);
            Contexto.SaveChanges();
        }

        public TEntidade ObterPorId(int id)
        {
            return Contexto.Set<TEntidade>().Find(id);
        }

        public List<TEntidade> ListarTodos()
        {
            return Contexto.Set<TEntidade>().ToList();
        }

        public async Task<TEntidade> AdicionarAssincrono(TEntidade entidade)
        {
            Contexto.Set<TEntidade>().Add(entidade);
            await Contexto.SaveChangesAsync();

            ProcessarEntidade(entidade);

            return entidade;
        }

        public async Task<TEntidade> AtualizarAssincrono(TEntidade entidade)
        {
            Contexto.Set<TEntidade>().Attach(entidade);
            Contexto.Entry(entidade).State = EntityState.Modified;
            await Contexto.SaveChangesAsync();

            return entidade;
        }

        public async Task RemoverAssincrono(TEntidade entidade)
        {
            Contexto.Set<TEntidade>().Remove(entidade);
            await Contexto.SaveChangesAsync();
        }

        public async Task<TEntidade> ObterPorIdAssincrono(int id)
        {
            return await Contexto.Set<TEntidade>().FindAsync(id);
        }

        public async Task<List<TEntidade>> ListarTodosAssincrono()
        {
            return await Contexto.Set<TEntidade>().ToListAsync();
        }

        private void ProcessarEntidade(TEntidade entidade)
        {
            var employee = entidade as Employee;
            if (employee != null)
                Recipente.Employees.Add(employee);
        }

    }
}