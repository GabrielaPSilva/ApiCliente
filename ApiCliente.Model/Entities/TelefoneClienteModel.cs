using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public ClienteModel? Cliente { get; set; }
        public TipoTelefoneModel? TipoTelefone { get; set; }

        public bool EhValido(out string mensagemErro)
        {
            bool ehValido = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            string mensagemErroIdTipoTelefone = string.Empty;
            ehValido = ehValido && ValidarIdTipoTelefone(out mensagemErroIdTipoTelefone);
            sbMensagemErro.Append(mensagemErroIdTipoTelefone);

            string mensagemErroTelefone = string.Empty;
            ehValido = ehValido && ValidarTelefone(out mensagemErroTelefone);
            sbMensagemErro.Append(mensagemErroTelefone);

            string mensagemErroCliente = string.Empty;
            ehValido = ehValido && (Cliente == null || Cliente.EhValido(out mensagemErroCliente));
            sbMensagemErro.Append(mensagemErroCliente);

            string mensagemErroTipoTelefone = string.Empty;
            ehValido = ehValido && (TipoTelefone == null || TipoTelefone.EhValido(out mensagemErroTipoTelefone));
            sbMensagemErro.Append(mensagemErroTipoTelefone);

            mensagemErro = sbMensagemErro.ToString();

            return ehValido;
        }

        private bool ValidarIdTipoTelefone(out string mensagemErro)
        {
            bool ehValido = IdTipoTelefone > 0;

            mensagemErro = string.Empty;

            if (!ehValido)
            {
                mensagemErro = "Informe um tipo telefone válido.\n";
            }

            return ehValido;
        }

        private bool ValidarTelefone(out string mensagemErro)
        {
            bool ehValido = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            string TelefoneRegexPattern = @"^\d{2}\d{8,9}$";

            ehValido = ehValido && !string.IsNullOrEmpty(Telefone);
            ehValido = ehValido && Telefone!.Length >= 1;
            ehValido = ehValido && Telefone!.Length <= 20;
            ehValido = ehValido && !Regex.IsMatch(Telefone!, TelefoneRegexPattern);

            mensagemErro = string.Empty;

            if (!ehValido)
                mensagemErro = "Informe um telefone válido.";

            return ehValido;
        }
    }
}
