using Biblioteca.Entities.Exceptions;
using System.Text.RegularExpressions;
using System.Globalization;
using Biblioteca.Services.Listas;
using Biblioteca.Services.Pagamentos;
using System.Xml;
namespace Biblioteca.Services
{
    class Program
    {
        static void Main(string[] args)
        {
            Usuario usuario = new Usuario();
            // Variaveis de listas e testes
            string arquivo = @"C:\Users\alexa\Desktop\Estoque\Livros.txt";
            List<Livro> listaDeLivros = Estoque.LerEarmazenarLivros(arquivo); // Armazena a lista de livros
            UserLists gerenciador = new UserLists();
            int dados = 0;


            while (dados == 0)
            {
                try
                {
                    Console.WriteLine("Seja Bem vindo a nossa biblioteca:");
                    Console.WriteLine("Cadastre seu usário");
                    Console.WriteLine("Digite seu primeiro nome: ");
                    string nome = Console.ReadLine();
                    Console.WriteLine("Digite um numero de identficação com 4 digitos: ");
                    int NumerodeIdentificação = int.Parse(Console.ReadLine());
                    Console.WriteLine("Seu Endereço: ");
                    string Endereço = Console.ReadLine();
                    Console.WriteLine("Digite seu telefone celular: (Deve Conter somente numeros e possuir 9 numeros)");
                    string telefone = Console.ReadLine();

                    usuario = new Usuario(nome, NumerodeIdentificação, Endereço, telefone);
                    gerenciador.AddElementosU(usuario);

                    Console.WriteLine();
                    Console.WriteLine("Dados do usuário: ");
                    foreach (Usuario A in gerenciador.listU)
                    {
                        Console.WriteLine(A);
                    }
                    Console.WriteLine();
                    Console.WriteLine("Os dados estão estão corretos? (digite apenas um caracter (n ou N ou s ou S)");
                    char verificação = char.Parse(Console.ReadLine());
                    if (verificação == 'S' || verificação == 's')
                    {
                        dados++;
                    }
                    else if (verificação == 'N' || verificação == 'n')
                    {
                        gerenciador.RemoveElementosU(usuario);
                        Console.WriteLine();
                    }
                    else 
                    {
                        gerenciador.RemoveElementosU(usuario);
                        Console.WriteLine("Digite digite apenas um caracter (n ou N ou s ou S)");
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Fique Atento ao que o programa sugere e informe corretamente os dados");
                }
                catch (ExcessõesPrograma ex)
                {
                    Console.WriteLine("Ocorreu um erro: " + ex.Message);
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Ocorreu um erro inesperado");
                }
            }





            // Usar os Livros

            BookLists livros = new BookLists();
            foreach (Livro livro in listaDeLivros)
            {
                livros.AddElementosL(livro);
            }

            Console.WriteLine();
            Console.WriteLine("Deseja ver as opções de titulo de livros? (digite apenas um caracter (n ou N ou s ou S)");

            // Exige que digite certo o caracter
            bool Bconfirmar1 = false;
            do
            {
                try
                {
                    char confirmar1 = char.Parse(Console.ReadLine());
                    if (confirmar1 == 'n' || confirmar1 == 'N')
                    {
                        return;
                    }
                    else if (confirmar1 == 's' || confirmar1 == 'S')
                    {
                        Bconfirmar1 = true;
                    }
                    else
                    {
                        Bconfirmar1 = false;
                        Console.WriteLine("Digite um caracter apenas s ou n ou S ou N!");
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Erro digite apenas os caracteres");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ocorreu um erro inesperado{e.Message}");
                }
            } while (Bconfirmar1 == false);

            Console.WriteLine("--------------------");
            Console.WriteLine();
            Console.WriteLine("Estas são as opções: ");
            Console.WriteLine();
            foreach (Livro livro in livros.ListL)
            {
                Console.Write(livro.Titulo + " ");
                Console.WriteLine("ISBN: " + livro.ISBN);
            }
            Console.WriteLine();


            Console.WriteLine("--------------------");
            Console.WriteLine();
            //Pede que o usuário escolha um modelo

            Console.WriteLine("Deseja algumas dessas Opções? (digite apenas um caracter (n ou N ou s ou S)");

            // Exige que digite certo o caracter
            bool Bconfirmar2 = false;
            do
            {
                try
                {
                    char confirmar2 = char.Parse(Console.ReadLine());
                    if (confirmar2 == 'n' || confirmar2 == 'N')
                    {
                        return;
                    }
                    else if (confirmar2 == 's' || confirmar2 == 'S')
                    {
                        Bconfirmar2 = true;
                    }
                    else
                    {
                        Bconfirmar2 = false;
                        Console.WriteLine("Digite um caracter apenas s ou n ou S ou N!");
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Erro digite apenas os caracteres");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ocorreu um erro inesperado{e.Message}");
                }
            } while (Bconfirmar2 == false);

            Console.WriteLine("--------------------");
            Console.WriteLine();

            Console.WriteLine("Escreva o nome e o ISBN do livro que você deseja");
            Console.WriteLine("(ATENÇÃO AS INFORMAÇÕES DO LIVRO DEVEM SER EXATAMENTE COMO FOI FORNECDIO SOMENTE COM ESPAÇO ENTRE ELES)");

            Livro livroPagamento = new Livro();
            int quantidade_livros = 0;
            double preço = 0;

            //Verificação de nome e isbn
            livroPagamento = null;
            bool Bnome_isbn = false;
            do
            {
                string[] nome_isbn = Console.ReadLine().Split(" ISBN: ");

                if (nome_isbn.Length == 2)
                {
                    string nomeLivro = nome_isbn[0];
                    string isbnLivro = nome_isbn[1];

                    Livro livroEncontrado = livros.ListL.Find(livro => livro.Titulo == nomeLivro && livro.ISBN == isbnLivro);
                    livroPagamento = livroEncontrado;
                }

                Console.WriteLine();
                if (livroPagamento != null)
                {
                    Console.WriteLine("----------------------------------");
                    Console.WriteLine("Livro Encontrado seguiremos para o processo de pagamento");
                    Console.WriteLine();
                    Console.WriteLine($"Este livro possui: {livroPagamento.Estoque} unidades");
                    Console.WriteLine($"Cada unidade custa {(livroPagamento.Preço / 100.0).ToString("F2", CultureInfo.InvariantCulture)}");
                    quantidade_livros = livroPagamento.Estoque;
                    preço = livroPagamento.Preço / 100.0;
                    Bnome_isbn = true;
                }
                else
                {
                    Console.WriteLine("Livro não encontrado.");
                    Console.WriteLine("Digite exatamente como fornecido o nome e o isbn!");
                    Console.WriteLine();
                    Bnome_isbn = false;
                }

            } while (Bnome_isbn == false);

            Console.WriteLine("--------------------");
            Console.WriteLine();

            Console.WriteLine();
            if (livroPagamento != null)
            {
                Console.WriteLine("Deseja comprar ou pegar emprestado? (ÃTENÇÃO O EMPRÉSTIMO MENSAL É SEMPRE 10% DO VALOR DO LIVRO)");
                Console.WriteLine("Escreva comprar ou emprestar");

                string metodoescolhido = "";

                bool Bmetodoescolhido = false;
                do
                {
                    try
                    {
                        metodoescolhido = Console.ReadLine();

                        if (metodoescolhido == "comprar" || metodoescolhido == "Comprar" || metodoescolhido == "Emprestar" || metodoescolhido == "emprestar")
                        {
                            Bmetodoescolhido = true;
                        }
                        else
                        {
                            Console.WriteLine("Digite comprar ou emprestar");
                        }
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Erro digite comprar ou emprestar");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Ocorreu um erro inesperado{e.Message}");
                    }
                } while (Bmetodoescolhido == false);


                Console.WriteLine("--------------------");
                Console.WriteLine();


                // comprar
                if (metodoescolhido == "comprar" || metodoescolhido == "Comprar")
                {

                    int quantidade = 0;
                    bool quantidadeValida = false;

                    do
                    {
                        Console.Write("Deseja comprar quantos livros: ");
                        string entradaQuantidade = Console.ReadLine();

                        try
                        {
                            quantidade = int.Parse(entradaQuantidade);

                            if (quantidade > 0 && livroPagamento.Estoque >= quantidade)
                            {
                                quantidadeValida = true;
                            }
                            else if (quantidade <= 0)
                            {
                                Console.WriteLine("A quantidade deve ser maior que zero.");
                            }
                            else
                            {
                                Console.WriteLine($"Não há estoque suficiente. Estoque disponível: {livroPagamento.Estoque}");
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Entrada inválida. Digite um número inteiro.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
                        }
                    } while (!quantidadeValida);


                    // validação para pix ou crédito



                    bool Bescolhadopagamento = false;
                    do
                    {


                        string escolhapagamento = ""; // Inicializa a variável com uma string vazia

                        do
                        {
                            Console.WriteLine("Quer pagar com pix ou com Crédito?");
                            Console.WriteLine("(Digite crédito ou pix)");

                            escolhapagamento = Console.ReadLine();

                            //valida se é pix ou crédito
                            if (escolhapagamento == "Pix" || escolhapagamento == "pix" || escolhapagamento == "crédito" || escolhapagamento == "Crédito")
                            {
                                Bescolhadopagamento = true;
                            }
                            else
                            {
                                Console.WriteLine("Digite corretamente crédito ou pix");
                            }

                        } while (!Bescolhadopagamento);

                        //compra no pix
                        if (escolhapagamento == "Pix" || escolhapagamento == "pix")
                        {
                            Pix pix = new Pix();
                            double px = pix.Pagamento(quantidade, livroPagamento);

                            Console.WriteLine();
                            Console.WriteLine($"Valor a Pagar: {px.ToString("F2", CultureInfo.InvariantCulture)}");

                        }
                        //comprar no crédito
                        else if (escolhapagamento == "crédito" || escolhapagamento == "Crédito")
                        {
                            Crédito credito = new Crédito();
                            double Credito = credito.Pagamento(quantidade_livros, livroPagamento);

                            Console.WriteLine();
                            Console.WriteLine($"Valor a Pagar: {Credito.ToString("F2", CultureInfo.InvariantCulture)}");


                        }
                        else
                        {
                        }

                    } while (Bescolhadopagamento == false);
                }


                // Pegar Emprestado
                else if (metodoescolhido == "emprestar" || metodoescolhido == "Emprestar")
                {
                    Console.WriteLine("Qual a data de Entrega? (NO MINIMO 1 MÊS)");
                    Console.WriteLine("<Atenção escreva dia/Mês/ano dessa forma>");

                    DateTime entrega = DateTime.Now;
                    bool dataValida = false;

                    do
                    {
                        try
                        {
                            entrega = DateTime.Parse(Console.ReadLine());

                            if (entrega <= DateTime.Now)
                            {
                                Console.WriteLine("A data de entrega deve ser no futuro.");
                            }
                            else if ((entrega - DateTime.Now).TotalDays < 30)
                            {
                                Console.WriteLine("A data de entrega deve ser de no mínimo 1 mês.");
                            }
                            else
                            {
                                dataValida = true;
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Formato de data inválido. Use dia/Mês/ano.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}");
                        }
                    } while (!dataValida);

                    TimeSpan diferenca = entrega - DateTime.Now;
                    int mesesAproximados = (int)(diferenca.TotalDays / 30.44);


                    bool Bescolhadopagamento = false;
                    string pagamentoemprestimo = ""; // Inicializa a variável com uma string vazia

                    do
                    {
                        Console.WriteLine("Quer pagar com pix ou com Crédito?");
                        Console.WriteLine("(Digite crédito ou pix)");

                        pagamentoemprestimo = Console.ReadLine();

                        if (pagamentoemprestimo == "Pix" || pagamentoemprestimo == "pix" || pagamentoemprestimo == "crédito" || pagamentoemprestimo == "Crédito")
                        {
                            Bescolhadopagamento = true;
                        }
                        else
                        {
                            Console.WriteLine("Digite corretamente crédito ou pix");
                        }

                    } while (!Bescolhadopagamento);

                    //Parcela(s) no Pix
                    if (pagamentoemprestimo == "Pix" || pagamentoemprestimo == "pix")
                    {
                        Emprestimo em = new Emprestimo(DateTime.Now, entrega, diferenca);
                        double mensalidadepix = em.MensalidadePix(livroPagamento);
                        Console.WriteLine($"Valor a pagar por mês {mensalidadepix.ToString("F4", CultureInfo.InvariantCulture)}");
                    }
                    // Parcela(s) no crédito
                    else if (pagamentoemprestimo == "crédito" || pagamentoemprestimo == "Crédito")
                    {
                        Emprestimo em = new Emprestimo(DateTime.Now, entrega, diferenca);
                        double mensalidadecredito = em.MensalidadeCredito(livroPagamento);
                        Console.WriteLine($"Valor a pagar por mês {mensalidadecredito.ToString("F4", CultureInfo.InvariantCulture)}");
                    }
                }


                Console.WriteLine();
                Console.WriteLine($"{usuario.Nome}Você será enchaminhado a sua intituição bancária muito obrigado :)");
            }
        }
    }
}