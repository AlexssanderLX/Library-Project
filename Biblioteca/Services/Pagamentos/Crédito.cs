using Biblioteca.Entities.Exceptions;

namespace Biblioteca.Services.Pagamentos
{
    class Crédito : Compra
    {
        public const double TaxaC = 0.05;
        public override double Pagamento(int quantidade, Livro livro)
        {
        
            double taxaCredito = base.Pagamento(quantidade, livro) * (1+ TaxaC);
            return taxaCredito/100;
        }
        public override double TaxaParcela(Livro livro)
        {
            return 0;
        }
    }
}
