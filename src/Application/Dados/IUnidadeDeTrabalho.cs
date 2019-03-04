using System;
using System.Data.Entity;

namespace Application.Dados
{
    public interface IUnidadeDeTrabalho<TContexto> : IDisposable
        where TContexto : DbContext
    {
        void Finalizar();
    }
}