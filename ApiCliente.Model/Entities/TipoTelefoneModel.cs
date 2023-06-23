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

        public bool IsValid(out string mensagemErro)
        {
            bool isValid = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            string mensagemErroNome = string.Empty;
            isValid = isValid && ValidarNomeTipo(out mensagemErroNome);
            sbMensagemErro.Append(mensagemErroNome);

            mensagemErro = sbMensagemErro.ToString();

            return isValid;
        }

        private bool ValidarNomeTipo(out string mensagemErro)
        {
            bool isValid = true;

            isValid = isValid && !string.IsNullOrEmpty(Tipo);
            isValid = isValid && Tipo!.Length > 1;
            isValid = isValid && Tipo!.Length <= 50;

            mensagemErro = string.Empty;

            if (!isValid)
                mensagemErro = "Informe um tipo de telefone válido, máximo 50 caracteres.\n";

            return isValid;
        }
    }
}
