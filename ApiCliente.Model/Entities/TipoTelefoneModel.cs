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

        public bool IsTrue(out string mensagemErro)
        {
            bool isTrue = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            string mensagemErroNome = string.Empty;
            isTrue = isTrue && ValidarNomeTipo(out mensagemErroNome);
            sbMensagemErro.Append(mensagemErroNome);

            mensagemErro = sbMensagemErro.ToString();

            return isTrue;
        }

        private bool ValidarNomeTipo(out string mensagemErro)
        {
            bool isTrue = true;

            isTrue = isTrue && !string.IsNullOrEmpty(Tipo);
            isTrue = isTrue && Tipo!.Length > 1;
            isTrue = isTrue && Tipo!.Length <= 50;

            mensagemErro = string.Empty;

            if (!isTrue)
                mensagemErro = "Informe um tipo de telefone válido, máximo 50 caracteres.\n";

            return isTrue;
        }
    }
}
