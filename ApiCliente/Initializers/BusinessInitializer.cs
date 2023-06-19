using ApiCliente.Business.Services;
using ApiCliente.Business.Services.Interfaces;

namespace ApiCliente.Initializers
{
    public class BusinessInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<ITelefoneClienteService, TelefoneClienteService>();
            services.AddTransient<ITipoTelefoneService, TipoTelefoneService>();
        }
    }
}
