using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCliente.Model.Entities
{
    public class TipoTelefoneModel
    {
        public int Id { get; set; }
        public string? Tipo { get; set; }
        public bool Ativo { get; set; }
    }
}
