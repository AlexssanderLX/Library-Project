using Biblioteca.Entities.Exceptions;

namespace Biblioteca.Services.Pagamentos
{
    interface IPagamentos
    {
        public double Pagamento(int quantidade, Livro livro);
        public double TaxaParcela(Livro livro);
    }
}
