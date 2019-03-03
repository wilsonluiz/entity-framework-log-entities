using System;
using System.Data.Entity;
using Application.Dados.Entidades;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace Application.Dados
{
    public class HrContext : DbContext
    {
        //public HrContext()
        //    : base(
        //        ObterConexao(
        //            "Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521)) (CONNECT_DATA = (SID = XE)));User Id=HR;Password=hr"),
        //        true)
        //{
        //}

        public HrContext(IConfiguration configuracao)
        : base (ObterConexao(configuracao["StringsConexao:HrContexto"]), true)
        {
            
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobHistory> JobHistories { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Location> Locations { get; set; }


        private static OracleConnection ObterConexao(string stringConexao)
        {
            if (string.IsNullOrEmpty(stringConexao))
                throw new ArgumentException($"Necessário informar a string de conexão para o contexto {nameof(HrContext)}");

            return new OracleConnection(stringConexao);
        }
    }
}