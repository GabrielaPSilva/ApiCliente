using ApiCliente.Data.Repositories.Interfaces;
using ApiCliente.Data.Repositories;
using ApiCliente.Data.DatabaseConnection.Interfaces;
using ApiCliente.Data.DatabaseConnection;

namespace ApiCliente.Initializers
{
    public class SessionInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<IDBSession, DBSession>();
        }
    }
}
