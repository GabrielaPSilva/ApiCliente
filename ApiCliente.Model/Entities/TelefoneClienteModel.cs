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
        public bool Ativo { get; set; }
        public string? Telefone { get; set; }

        public ClienteModel? Cliente { get; set; }
        public TipoTelefoneModel? TipoTelefone { get; set; }

        public bool IsTrue(out string mensagemErro)
        {
            bool isTrue = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            string mensagemErroIdTipoTelefone = string.Empty;
            isTrue = isTrue && ValidarIdTipoTelefone(out mensagemErroIdTipoTelefone);
            sbMensagemErro.Append(mensagemErroIdTipoTelefone);

            string mensagemErroTelefone = string.Empty;
            isTrue = isTrue && ValidarTelefone(out mensagemErroTelefone);
            sbMensagemErro.Append(mensagemErroTelefone);

            string mensagemErroCliente = string.Empty;
            isTrue = isTrue && (Cliente == null || Cliente.IsTrue(out mensagemErroCliente));
            sbMensagemErro.Append(mensagemErroCliente);

            string mensagemErroTipoTelefone = string.Empty;
            isTrue = isTrue && (TipoTelefone == null || TipoTelefone.IsTrue(out mensagemErroTipoTelefone));
            sbMensagemErro.Append(mensagemErroTipoTelefone);

            mensagemErro = sbMensagemErro.ToString();

            return isTrue;
        }

        private bool ValidarIdCliente(out string mensagemErro)
        {
            bool isTrue = IdCliente > 0;

            mensagemErro = string.Empty;

            if (!isTrue)
            {
                mensagemErro = "Informe um tipo telefone válido.\n";
            }

            return isTrue;
        }

        private bool ValidarIdTipoTelefone(out string mensagemErro)
        {
            bool isTrue = IdTipoTelefone > 0;

            mensagemErro = string.Empty;

            if (!isTrue)
            {
                mensagemErro = "Informe um tipo telefone válido.\n";
            }

            return isTrue;
        }

        private bool ValidarTelefone(out string mensagemErro)
        {
            bool isTrue = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            string TelefoneRegexPattern = @"^\d{2}\d{8,9}$";

            isTrue = isTrue && !string.IsNullOrEmpty(Telefone);
            isTrue = isTrue && Telefone!.Length >= 1;
            isTrue = isTrue && Telefone!.Length <= 20;
            isTrue = isTrue && Regex.IsMatch(Telefone!, TelefoneRegexPattern);

            mensagemErro = string.Empty;

            if (!isTrue)
                mensagemErro = "Informe um telefone válido.";

            return isTrue;
        }
    }
}
