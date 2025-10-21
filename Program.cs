using System;

class Program
{
    static void Main(string[] args)
    {
        Menu();
    }

    static void Menu()
    {
        int opcao = 0;

        do
        {
            Creditos();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Controle de Estoque");
            Console.ResetColor();

            Console.WriteLine("Pressione a tecla correspondente a sua escolha:");
            Console.WriteLine("1 - Listar produtos");
            Console.WriteLine("2 - Cadastrar produto");
            Console.WriteLine("3 - Editar produto");
            Console.WriteLine("4 - Excluir produto");
            Console.WriteLine("5 - Dar ENTRADA em estoque");
            Console.WriteLine("6 - Dar SA�DA de estoque");
            Console.WriteLine("7 - Relat�rio: Estoque abaixo do m�nimo");
            Console.WriteLine("8 - Relat�rio: Extrato de movimentos por produto");
            Console.WriteLine("9 - Salvar(CSV)");
            Console.WriteLine("0 - Sair");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.ResetColor();
            Console.Write("Opção: ");

            int.TryParse(Console.ReadLine(), out opcao);
            Console.Clear();

            switch (opcao)
            {
                case 1:
                    //Listar produtos
                    break;
                case 2:
                    //Cadastrar produto
                    break;
                case 3:
                    //Editar produto
                    break;
                case 4:
                    //Excluir produto
                    break;
                case 5:
                    //Dar ENTRADA em estoque
                    break;
                case 6:
                    //Dar SA�DA de estoque
                    break;
                case 7:
                    //Relat�rio: Estoque abaixo do m�nimo
                    break;
                case 8:
                    //Relat�rio: Extrato de movimentos por produto
                    break;
                case 9:
                    //Salvar(CSV)
                    break;
                case 0:
                    //Sair
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }

            Console.Clear();

        } while (opcao != 5);
    }

    static void Creditos()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Turma: 2ESCN");
        Console.ResetColor();

        Console.WriteLine("Desenvolvedores:");
        Console.WriteLine("Ariane da Silva Archanjo - 2025106857");
        Console.WriteLine("Caio Melo Canhetti - 2025104636");
        Console.WriteLine("Lucas Vinicius Barros Dias - 2025105450");
        Console.WriteLine("Matheus Sizanoski Figueiredo - 2025105007");
        Console.WriteLine("Pedro Henrique Kafka Zaratino - 2025105057");
        Console.WriteLine("Rafael Martins Schreurs Sales - 2025105454");

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.ResetColor();

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
        Console.Clear();
    }
}

