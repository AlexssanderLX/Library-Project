using Biblioteca.Services;
namespace Biblioteca.Entities.Exceptions
{
    public class Livro
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public string Genero { get; set; }
        public int Estoque { get; set; }
        public double Preço { get; set; }

        public Livro() 
        {
        }
        public static bool ISBN_Valido(string isbn)
        {
            isbn = isbn.Replace("-", "").Replace(" ", "");
            if(isbn.Length != 13)
            {
                return false;
            }

            int soma = 0;
            for (int i = 0; i < 12; i++)
            {
                if (!char.IsDigit(isbn[i]))
                {
                    return false;
                }
                soma += (isbn[i] - '0') * (i % 2 == 0 ? 1 : 3);
            }
            int digitoVerificadorCalculado = (10 - (soma % 10)) % 10;
            return digitoVerificadorCalculado == (isbn[12] - '0');
        }
        
        public Livro(string titulo, string autor, string iSBN, string genero, int estoque, double preço)
        {
            ISBN_Valido(iSBN);
            Titulo = titulo;
            Autor = autor;
            ISBN = iSBN;
            Genero = genero;
            Estoque = estoque;
            Preço = preço;
        }

       
        public override string ToString()
        {
            return $"Titulo: {Titulo}, Autor: {Autor}, ISBN: {ISBN}, Ano de publicação {Genero}, Estoque: {Estoque}, Preço: {Preço/100.0}";
        }
    }
}