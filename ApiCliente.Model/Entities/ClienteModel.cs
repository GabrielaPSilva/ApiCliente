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

        public bool IsValid(out string mensagemErro)
        {
            bool isValid = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            string mensagemErroNome = string.Empty;
            isValid = isValid && ValidarNome(out mensagemErroNome);
            sbMensagemErro.Append(mensagemErroNome);

            string mensagemErroEmail = string.Empty;
            isValid = isValid && ValidarEmail(out mensagemErroEmail);
            sbMensagemErro.Append(mensagemErroEmail);

            string mensagemErroListaTelefones = string.Empty;
            isValid = isValid && ValidarListaTelefones(out mensagemErroListaTelefones);
            sbMensagemErro.Append(mensagemErroListaTelefones);

            mensagemErro = sbMensagemErro.ToString();

            return isValid;
        }

        private bool ValidarNome(out string mensagemErro)
        {
            bool isValid = true;
            isValid = isValid && !string.IsNullOrEmpty(Nome);
            isValid = isValid && Nome!.Length >= 1;
            isValid = isValid && Nome!.Length <= 100;

            mensagemErro = string.Empty;

            if (!isValid)
                mensagemErro = "O nome deve ter entre 1 e 100 caracteres.\n";

            return isValid;
        }

        private bool ValidarEmail(out string mensagemErro)
        {
            const string EmailRegexPattern = @"^[\w\.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*(\.[a-zA-Z]{2,})$";

            bool isValid = true;
            isValid = isValid && !string.IsNullOrEmpty(Email);
            isValid = isValid && Email!.Length >= 1;
            isValid = isValid && Email!.Length <= 100;
            isValid = isValid && Regex.IsMatch(Email!, EmailRegexPattern);

            mensagemErro = string.Empty;

            if(!isValid)
                mensagemErro = "Informe um endereço de e-mail válido.";

            return isValid;
        }

        private bool ValidarListaTelefones(out string mensagemErro)
        {
            bool isValid = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            this.ListaTelefones!.ForEach((telefone) =>
            {
                string mensagemErroTelefone = string.Empty;
                isValid = isValid && telefone.IsValid(out mensagemErroTelefone);
                sbMensagemErro.Append(mensagemErroTelefone);
            });

            mensagemErro = sbMensagemErro.ToString();

            return isValid;
        }
    }
}
