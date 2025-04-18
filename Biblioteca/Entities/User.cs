using System.Text.RegularExpressions;

namespace Biblioteca.Entities.Exceptions
{
    public class Usuario
    {
        public string Nome { get; set; }
        public int NumeroIdentificacao { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
       
        public static void ValidarNumeroIdentificacao(int numerodeidentificação)
        {
            if (numerodeidentificação < 1000 || numerodeidentificação > 9999)
            {
                throw new ExcessõesPrograma("Erro deve conter 4 digitos");
            }
        }
        public static void ValidarTamanhoTelefone(string telefone)
        {
            if (telefone.Length != 9)
            {
                throw new ExcessõesPrograma("Erro deve conter 9 digitos");

            }
            if (Regex.IsMatch(telefone, @"\D")) // \D corresponde a qualquer caractere não numérico
            {
                throw new ExcessõesPrograma("Telefone inválido. Deve conter apenas números.");
            }
        }
        public Usuario()
        {
        }
        public Usuario(string nome, int numeroIdentificacao, string endereco, string telefone)
        {
            ValidarNumeroIdentificacao(numeroIdentificacao);
            ValidarTamanhoTelefone(telefone);
            Nome = nome;
            NumeroIdentificacao = numeroIdentificacao;
            Endereco = endereco;
            Telefone = telefone;

        }
        public override string ToString()
        {
            return $"Seu nome: {Nome}, seu número de identificação: {NumeroIdentificacao}, seu endereço: {Endereco}, seu telefone: {Telefone} :)";
        }
    }
}
