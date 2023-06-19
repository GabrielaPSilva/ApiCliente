using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Model.Entities
{
    public class TelefoneClienteModel
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoTelefone { get; set; }
        public string? Telefone { get; set; }
        public bool Ativo { get; set; }
    }
}
