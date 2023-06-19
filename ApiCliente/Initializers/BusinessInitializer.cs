using ApiCliente.Business.Services;
using ApiCliente.Business.Services.Interfaces;

namespace ApiCliente.Initializers
{
    public class BusinessInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            //Significa que uma nova instância de serviço é criada cada vez que for solicitada, é adequado para serviços leves e sem estado, onde não há necessidade de manter estado ou compartilhamento de recursos.
            services.AddTransient<IClienteService, ClienteService>();            services.AddTransient<ITelefoneClienteService, TelefoneClienteService>();
            services.AddTransient<ITipoTelefoneService, TipoTelefoneService>();
        }
    }
}
