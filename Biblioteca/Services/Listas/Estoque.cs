using Biblioteca.Entities.Exceptions;
namespace Biblioteca.Services.Listas
{
    class Estoque
    {
        public static List<Livro> LerEarmazenarLivros(string Local)
        {
            List<Livro> listaDeLivros = new List<Livro>();

            try
            {
                using (FileStream fs = new FileStream(Local, FileMode.Open, FileAccess.Read))
                using (StreamReader sr = new StreamReader(fs))
                {
                    string linha;
                    while ((linha = sr.ReadLine()) != null)
                    {
                        if (linha.StartsWith("Título: "))
                        {
                            string titulo = linha.Substring("Título: ".Length);
                            string autor = sr.ReadLine()?.Substring("Autor: ".Length);
                            string isbn = sr.ReadLine()?.Substring("ISBN: ".Length);
                            string ano = sr.ReadLine()?.Substring("Gênero: ".Length);
                            int quantidadeEmEstoque = int.Parse(sr.ReadLine()?.Substring("Quantidade em Estoque: ".Length));
                            double preco = double.Parse(sr.ReadLine()?.Substring("Preço: ".Length));

                            if (autor != null && isbn != null)
                            {
                                Livro livro = new Livro(titulo, autor, isbn, ano, quantidadeEmEstoque, preco);
                                listaDeLivros.Add(livro);
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Erro: Arquivo não encontrado - {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Erro de E/S: {ex.Message}");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Erro: Formato de dados inválido - {ex.Message}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Erro: Índice fora do intervalo - {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
            }

            return listaDeLivros;
        }
    }
}