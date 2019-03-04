using System;
using System.Data.Entity;

namespace Application.Dados
{
    public interface IUnidadeDeTrabalho : IDisposable
    {
        void Finalizar();
    }

    public class UnidadeDeTrabalho<TContexto> : IUnidadeDeTrabalho where TContexto : DbContext
    {
        private TContexto _contexto;
        private DbContextTransaction _transacao;

        public UnidadeDeTrabalho(TContexto contexto)
        {
            _contexto = contexto;
            _transacao = _contexto.Database.BeginTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Finalizar()
        {
            if (_transacao.UnderlyingTransaction.Connection == null) return;

            _transacao.Commit();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            if (_transacao != null)
            {
                _transacao.Dispose();
                _transacao = null;
            }

            if (_contexto == null) return;

            _contexto.Dispose();
            _contexto = null;
        }
    }
}