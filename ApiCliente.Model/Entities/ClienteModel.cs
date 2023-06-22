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
        public TelefoneClienteModel? TelefoneCliente { get; set; }

        public bool EhValido(out string mensagemErro)
        {
            bool ehValido = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            string mensagemErroNome = string.Empty;
            ehValido = ehValido && ValidarNome(out mensagemErroNome);
            sbMensagemErro.Append(mensagemErroNome);

            string mensagemErroEmail = string.Empty;
            ehValido = ehValido && ValidarEmail(out mensagemErroEmail);
            sbMensagemErro.Append(mensagemErroEmail);

            string mensagemErroListaTelefones = string.Empty;
            ehValido = ehValido && ValidarListaTelefones(out mensagemErroListaTelefones);
            sbMensagemErro.Append(mensagemErroListaTelefones);

            mensagemErro = sbMensagemErro.ToString();

            return ehValido;
        }

        private bool ValidarNome(out string mensagemErro)
        {
            bool ehValido = true;
            ehValido = ehValido && !string.IsNullOrEmpty(Nome);
            ehValido = ehValido && Nome!.Length >= 1;
            ehValido = ehValido && Nome!.Length <= 100;

            mensagemErro = string.Empty;

            if (!ehValido)
                mensagemErro = "O nome deve ter no máximo 100 caracteres.\n";

            return ehValido;
        }

        private bool ValidarEmail(out string mensagemErro)
        {
            const string EmailRegexPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";

            bool ehValido = true;
            ehValido = ehValido && !string.IsNullOrEmpty(Email);
            ehValido = ehValido && Email!.Length >= 1;
            ehValido = ehValido && Email!.Length <= 100;
            ehValido = ehValido && !Regex.IsMatch(Email!, EmailRegexPattern);

            mensagemErro = string.Empty;

            if(!ehValido)
                mensagemErro = "Informe um endereço de e-mail válido.";

            return ehValido;
        }

        private bool ValidarListaTelefones(out string mensagemErro)
        {
            bool ehValido = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            this.ListaTelefones!.ForEach((telefone) =>
            {
                string mensagemErroTelefone = string.Empty;
                ehValido = ehValido && telefone.EhValido(out mensagemErroTelefone);
                sbMensagemErro.Append(mensagemErroTelefone);
            });

            mensagemErro = sbMensagemErro.ToString();

            return ehValido;
        }
    }
}
