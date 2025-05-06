namespace Rommanel.Tests.Helper
{
    public static class GerarCpfValido
    {
        public static string GerarCpf()
        {
            var rand = new Random();
            int[] cpf = new int[11];

            for (int i = 0; i < 9; i++)
                cpf[i] = rand.Next(10);

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += cpf[i] * (10 - i);

            int resto = soma % 11;
            cpf[9] = (resto < 2) ? 0 : 11 - resto;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += cpf[i] * (11 - i);

            resto = soma % 11;
            cpf[10] = (resto < 2) ? 0 : 11 - resto;

            return string.Join("", cpf);
        }
    }
}
