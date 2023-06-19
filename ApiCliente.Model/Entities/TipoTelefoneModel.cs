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

        public bool EhValido(out string mensagemErro)
        {
            bool ehValido = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            string mensagemErroNome = string.Empty;
            ehValido = ehValido && ValidarNomeTipo(out mensagemErroNome);
            sbMensagemErro.Append(mensagemErroNome);

            mensagemErro = sbMensagemErro.ToString();

            return ehValido;
        }

        private bool ValidarNomeTipo(out string mensagemErro)
        {
            bool ehValido = true;

            ehValido = ehValido && !string.IsNullOrEmpty(Tipo);
            ehValido = ehValido && Tipo!.Length > 1;
            ehValido = ehValido && Tipo!.Length <= 100;

            mensagemErro = string.Empty;

            if (!ehValido)
                mensagemErro = "Informe um tipo de telefone válido.\n";

            return ehValido;
        }
    }
}
