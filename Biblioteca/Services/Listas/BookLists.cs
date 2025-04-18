using Biblioteca.Entities.Exceptions;

namespace Biblioteca.Services.Listas
{
    class BookLists
    {
        public List<Livro> ListL = new List<Livro>();

        public void AddElementosL(Livro livro)//Adiciona elementos na lista
        {
            ListL.Add(livro);
        }
        public void RemoveElementosL(Livro livro)//Remove elementos da lista
        {
            ListL.Remove(livro);
        }

        public Livro BuscarporLivro(string titulo)
        {
            if (titulo == null)
            {
                throw new ExcessõesPrograma("Usário inválido");
            }
            return ListL.Find(u => u.Titulo == titulo)
                ?? throw new ExcessõesPrograma("usuário não existe");
        }
        public void CarregarLivrosDoArquivo(string caminhoDoArquivo)
        {
            List<Livro> livrosDoArquivo = Estoque.LerEarmazenarLivros(caminhoDoArquivo);

            foreach (Livro livro in livrosDoArquivo)
            {
                AddElementosL(livro); // Adiciona cada livro à lista ListL
            }
        }
    }
}
