namespace Poc.BoletoSre2.Helpers {

    public static class Extensoes {

        /// <summary>
        /// Retorna texto com apenas os dígitos...
        /// </summary>
        /// <param name="self">Texto a ser tratado.</param>
        /// <returns>Retorna nova string contendo apenas os dígitos do texto original.</returns>
        public static string ObterDigitos(this string self) {

            if (string.IsNullOrWhiteSpace(self) == true) { return self; }

            // Retornar apenas caracteres que são digitos...
            return new string(self.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// Verifica se o texto informado corresponde a um CNPJ válido...
        /// </summary>
        /// <param name="self">Texto a ser validado.</param>
        /// <returns>Retorna TRUE se o texto corresponde a um CNPJ válido.</returns>
        public static bool EhCnpjValido(this string self) {

            // Sai do método caso a string seja inválida.
            if (string.IsNullOrWhiteSpace(self) == true) { return false; }

            string cnpj = ObterDigitos(self);

            // Valida tamanho do CNPJ...
            if (cnpj.Length != 14 || cnpj.Distinct().Count() == 1) { return false; }

            string tempCnpj = cnpj.Substring(0, 12);

            // Calcular dígito verificador...
            string digit = CalcularDigitoCnpj(tempCnpj);

            // Calcula o segundo dígito verificador.
            digit += CalcularDigitoCnpj(tempCnpj + digit);

            return cnpj.EndsWith(digit);
        }


        private static string CalcularDigitoCnpj(string texto) {

            int[] multiplicador = new int[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            if (texto.Length == 13) { multiplicador = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 }; }
            int soma = 0;

            // Calcular os 9 primeiros dígitos...
            for (int i = 0; i < texto.Length; i++) {
                soma += int.Parse(texto[i].ToString()) * multiplicador[i];
            }
            int resto = soma % 11;

            // Valores menores que 2 converte para zero...
            resto = (resto < 2) ? 0 : 11 - resto;

            return resto.ToString();
        }

    }
}
