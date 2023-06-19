using ApiCliente.Data.Repositories;
using ApiCliente.Data.Repositories.Interfaces;

namespace ApiCliente.Initializers
{
    public class DataInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<ITelefoneClienteRepository, TelefoneClienteRepository>();
            services.AddTransient<ITipoTelefoneRepository, TipoTelefoneRepository>();
        }
    }
}
