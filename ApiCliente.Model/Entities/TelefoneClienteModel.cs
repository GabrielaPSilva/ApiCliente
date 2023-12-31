﻿using System;
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

        public bool IsValid(out string mensagemErro)
        {
            bool isValid = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            string mensagemErroIdTipoTelefone = string.Empty;
            isValid = isValid && ValidarIdTipoTelefone(out mensagemErroIdTipoTelefone);
            sbMensagemErro.Append(mensagemErroIdTipoTelefone);

            string mensagemErroTelefone = string.Empty;
            isValid = isValid && ValidarTelefone(out mensagemErroTelefone);
            sbMensagemErro.Append(mensagemErroTelefone);

            string mensagemErroCliente = string.Empty;
            isValid = isValid && (Cliente == null || Cliente.IsValid(out mensagemErroCliente));
            sbMensagemErro.Append(mensagemErroCliente);

            string mensagemErroTipoTelefone = string.Empty;
            isValid = isValid && (TipoTelefone == null || TipoTelefone.IsValid(out mensagemErroTipoTelefone));
            sbMensagemErro.Append(mensagemErroTipoTelefone);

            mensagemErro = sbMensagemErro.ToString();

            return isValid;
        }

        private bool ValidarIdCliente(out string mensagemErro)
        {
            bool isValid = IdCliente > 0;

            mensagemErro = string.Empty;

            if (!isValid)
            {
                mensagemErro = "Informe um tipo telefone válido.\n";
            }

            return isValid;
        }

        private bool ValidarIdTipoTelefone(out string mensagemErro)
        {
            bool isValid = IdTipoTelefone > 0;

            mensagemErro = string.Empty;

            if (!isValid)
            {
                mensagemErro = "Informe um tipo telefone válido.\n";
            }

            return isValid;
        }

        private bool ValidarTelefone(out string mensagemErro)
        {
            bool isValid = true;
            StringBuilder sbMensagemErro = new StringBuilder();

            string TelefoneRegexPattern = @"^\d{2}\d{8,9}$";

            isValid = isValid && !string.IsNullOrEmpty(Telefone);
            isValid = isValid && Telefone!.Length >= 1;
            isValid = isValid && Telefone!.Length <= 20;
            isValid = isValid && Regex.IsMatch(Telefone!, TelefoneRegexPattern);

            mensagemErro = string.Empty;

            if (!isValid)
                mensagemErro = "Informe um telefone válido.";

            return isValid;
        }
    }
}
