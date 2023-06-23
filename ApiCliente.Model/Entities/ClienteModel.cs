using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiCliente.Model.Entities
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public bool Ativo { get; set; }

        public List<TelefoneClienteModel>? ListaTelefones { get; set; }

        public bool IsTrue(out string mensagemErro)
        {
            bool isTrue = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            string mensagemErroNome = string.Empty;
            isTrue = isTrue && ValidarNome(out mensagemErroNome);
            sbMensagemErro.Append(mensagemErroNome);

            string mensagemErroEmail = string.Empty;
            isTrue = isTrue && ValidarEmail(out mensagemErroEmail);
            sbMensagemErro.Append(mensagemErroEmail);

            string mensagemErroListaTelefones = string.Empty;
            isTrue = isTrue && ValidarListaTelefones(out mensagemErroListaTelefones);
            sbMensagemErro.Append(mensagemErroListaTelefones);

            mensagemErro = sbMensagemErro.ToString();

            return isTrue;
        }

        private bool ValidarNome(out string mensagemErro)
        {
            bool isTrue = true;
            isTrue = isTrue && !string.IsNullOrEmpty(Nome);
            isTrue = isTrue && Nome!.Length >= 1;
            isTrue = isTrue && Nome!.Length <= 100;

            mensagemErro = string.Empty;

            if (!isTrue)
                mensagemErro = "O nome deve ter entre 1 e 100 caracteres.\n";

            return isTrue;
        }

        private bool ValidarEmail(out string mensagemErro)
        {
            const string EmailRegexPattern = @"^[\w\.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,})$";

            bool isTrue = true;
            isTrue = isTrue && !string.IsNullOrEmpty(Email);
            isTrue = isTrue && Email!.Length >= 1;
            isTrue = isTrue && Email!.Length <= 100;
            isTrue = isTrue && Regex.IsMatch(Email!, EmailRegexPattern);

            mensagemErro = string.Empty;

            if(!isTrue)
                mensagemErro = "Informe um endereço de e-mail válido.";

            return isTrue;
        }

        private bool ValidarListaTelefones(out string mensagemErro)
        {
            bool isTrue = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            this.ListaTelefones!.ForEach((telefone) =>
            {
                string mensagemErroTelefone = string.Empty;
                isTrue = isTrue && telefone.IsTrue(out mensagemErroTelefone);
                sbMensagemErro.Append(mensagemErroTelefone);
            });

            mensagemErro = sbMensagemErro.ToString();

            return isTrue;
        }
    }
}
