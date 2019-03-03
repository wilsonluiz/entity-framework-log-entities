using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Application.Auxiliares;
using Application.Dados.Entidades;

namespace Application.Dados.Repositorios
{
    public class RepositorioEmployee : RepositorioBase<HrContext, Employee>, IRepositorioEmployee
    {
        public RepositorioEmployee(HrContext contexto)
            : base(contexto)
        {
        }

        public async Task<List<Employee>> ListarEmployeesdAssincrono(ParametrosEmployee parametros)
        {
            var consulta = Contexto.Employees.AsQueryable();

            if (parametros?.DepartmentId != null)
                consulta = consulta.Where(e => e.DepartmentId == parametros.DepartmentId);

            if (parametros?.ManagerId != null)
                consulta = consulta.Where(e => e.ManagerId == parametros.ManagerId);

            if (!string.IsNullOrEmpty(parametros?.JobId))
                consulta = consulta.Where(e => e.JobId.Equals(parametros.JobId));

            return await consulta.ToListAsync();
        }
    }
}