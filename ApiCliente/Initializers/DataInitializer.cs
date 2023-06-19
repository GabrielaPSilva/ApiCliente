using ApiCliente.Data.Repositories;
using ApiCliente.Data.Repositories.Interfaces;

namespace ApiCliente.Initializers
{
    public class DataInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            //Significa que uma nova instância de serviço é criada cada vez que for solicitada, é adequado para serviços leves e sem estado, onde não há necessidade de manter estado ou compartilhamento de recursos.
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<ITelefoneClienteRepository, TelefoneClienteRepository>();
            services.AddTransient<ITipoTelefoneRepository, TipoTelefoneRepository>();
        }
    }
}
