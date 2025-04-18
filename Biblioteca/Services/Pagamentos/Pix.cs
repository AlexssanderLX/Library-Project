using Biblioteca.Entities.Exceptions;

namespace Biblioteca.Services.Pagamentos
{
    class Pix : Compra
    {
        public const double TaxaP = 0.02;
        public  override double Pagamento(int quantidade, Livro livro)
        {
            double taxaPix = base.Pagamento(quantidade, livro) * (1 + TaxaP);
            return taxaPix/100;    
        }
        public override double TaxaParcela(Livro livro)
        {
            return 0;
        }
    }
}
