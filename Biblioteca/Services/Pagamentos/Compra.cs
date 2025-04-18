using Biblioteca.Entities.Exceptions;

namespace Biblioteca.Services.Pagamentos
{
    abstract class Compra : IPagamentos
    {
        public virtual double Pagamento(int quantidade, Livro livro) 
        {
            return livro.Preço * quantidade;
        }

        public abstract double TaxaParcela(Livro livro);
    }
}
