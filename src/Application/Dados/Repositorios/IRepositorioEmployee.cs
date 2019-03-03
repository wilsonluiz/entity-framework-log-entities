using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Auxiliares;
using Application.Dados.Entidades;

namespace Application.Dados.Repositorios
{
    public interface IRepositorioEmployee : IRepositorioBase<HrContext, Employee>
    {
        Task<List<Employee>> ListarEmployeesdAssincrono(ParametrosEmployee parametros);
    }
}