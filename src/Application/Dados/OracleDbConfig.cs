using System.Data.Entity;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.EntityFramework;

namespace Application.Dados
{
    public class OracleDbConfig : DbConfiguration
    {
        public static readonly DbConfiguration Instancia = new OracleDbConfig();

        public OracleDbConfig()
        {
            SetDefaultConnectionFactory(new OracleConnectionFactory());
            SetProviderServices("Oracle.ManagedDataAccess.Client", EFOracleProviderServices.Instance);
            SetProviderFactory("Oracle.ManagedDataAccess.Client", new OracleClientFactory());
            SetDatabaseInitializer<HrContext>(null);
            SetDatabaseInitializer<HrSimuladoContext>(null);

            //AddInterceptor(new EntityMonitorInterceptor());
        }
    }
}
