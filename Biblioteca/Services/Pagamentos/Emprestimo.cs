using Biblioteca.Entities.Exceptions;
using System;

namespace Biblioteca.Services.Pagamentos
{

     class Emprestimo : Compra
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataEntrega { get; set; }
        public TimeSpan Duração { get; set; }

        public Emprestimo(DateTime dataInicio, DateTime dataEntrega, TimeSpan duração)
        {
            DataInicio = dataInicio;
            DataEntrega = dataEntrega;
            Duração = duração;
        }

        public override double Pagamento(int quantidade, Livro livro)
        {
            return livro.Preço;
        }
        public override double TaxaParcela(Livro livro)
        {
            return 0.10;
        }

        public double MensalidadeCredito(Livro livro)
        {

            int meses = CalculoMeses();
            double valorM = livro.Preço * (1 + TaxaParcela(livro) * meses);
            double valorM2 = livro.Preço * (Crédito.TaxaC * meses);
            double valorM3 = valorM2 + valorM;
            double mensalidade = valorM / meses;
            return mensalidade/100;
        }
        public double MensalidadePix(Livro livro)
        {
           
            int meses = CalculoMeses();
            double valorM = livro.Preço * (1 + TaxaParcela(livro) * meses);
            double valorM2 = livro.Preço * (Pix.TaxaP * meses);
            double valorM3 = valorM2+ valorM;
            double mensalidade = valorM3 / meses;
            return mensalidade/100;
        }
        
        private int CalculoMeses()
        {
            int meses = ((DataEntrega.Year - DataInicio.Year) * 12) + DataEntrega.Month - DataInicio.Month;
            return Math.Max(meses, 1);
        }    
    }
}