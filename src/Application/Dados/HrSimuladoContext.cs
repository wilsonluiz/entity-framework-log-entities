using System;
using System.Data.Entity;
using Application.Dados.Configuracoes;
using Application.Dados.Entidades;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace Application.Dados
{
    public class HrSimuladoContext : DbContext
    {
        public HrSimuladoContext(IConfiguration configuracao)
            : base(ObterConexaoOracle(configuracao["StringsConexao:HrSimuladoContexto"]), true)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        private static OracleConnection ObterConexaoOracle(string stringConexao)
        {
            if (string.IsNullOrEmpty(stringConexao))
                throw new ArgumentException(
                    $"Necessário informar string de conexão para o contexto {nameof(HrSimuladoContext)}");

            return new OracleConnection(stringConexao);
        }
    }
}