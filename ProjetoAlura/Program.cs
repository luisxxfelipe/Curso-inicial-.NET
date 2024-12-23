using System;
using System.Collections.Generic;

namespace ScreenSoundApp
{
    public class Program
    {
        static String mensagemBoasVindas = "Bem vindo ao Screen Sound";

        static Dictionary<string, Banda> bandasListadas = new Dictionary<string, Banda>();

        public class Banda
        {
            public string Nome { get; set; }
            public string Genero { get; set; }
            public List<int> Avaliacoes { get; set; }

            public Banda(string nome, string genero)
            {
                Nome = nome;
                Genero = genero;
                Avaliacoes = new List<int>();
            }

            public double ObterMediaAvaliacoes()
            {
                if (Avaliacoes.Count == 0)
                    return 0;
                double soma = 0;
                foreach (var avaliacao in Avaliacoes)
                    soma += avaliacao;
                return soma / Avaliacoes.Count;
            }
        }

        public static void Main(string[] args)
        {
            ExibirMenu();
        }

        static void ExibirLogo()
        {
            Console.WriteLine(@"
░██████╗░█████╗░██████╗░███████╗███████╗███╗░░██╗  ░██████╗░█████╗░██╗░░░██╗███╗░░██╗██████╗░
██╔════╝██╔══██╗██╔══██╗██╔════╝██╔════╝████╗░██║  ██╔════╝██╔══██╗██║░░░██║████╗░██║██╔══██╗
╚█████╗░██║░░╚═╝██████╔╝█████╗░░█████╗░░██╔██╗██║  ╚█████╗░██║░░██║██║░░░██║██╔██╗██║██║░░██║
░╚═══██╗██║░░██╗██╔══██╗██╔══╝░░██╔══╝░░██║╚████║  ░╚═══██╗██║░░██║██║░░░██║██║╚████║██║░░██║
██████╔╝╚█████╔╝██║░░██║███████╗███████╗██║░╚███║  ██████╔╝╚█████╔╝╚██████╔╝██║░╚███║██████╔╝
╚═════╝░░╚════╝░╚═╝░░╚═╝╚══════╝╚══════╝╚═╝░░╚══╝  ╚═════╝░░╚════╝░░╚═════╝░╚═╝░░╚══╝╚═════╝░
");
            Console.WriteLine(mensagemBoasVindas);
        }

        static void RegistrarBandas()
        {
            Console.Clear();
            TitulosOpcoes("REGISTRANDO UMA NOVA BANDA");

            Console.Write("Digite o nome da banda: ");
            string nomeBanda = Console.ReadLine();

            Console.Write("Digite o gênero da banda: ");
            string generoBanda = Console.ReadLine();

            if (!bandasListadas.ContainsKey(nomeBanda))
            {
                Banda novaBanda = new Banda(nomeBanda, generoBanda);

                Console.Write("Digite uma avaliação inicial (ou pressione Enter para pular): ");
                string inputAvaliacao = Console.ReadLine();
                if (int.TryParse(inputAvaliacao, out int avaliacao))
                {
                    novaBanda.Avaliacoes.Add(avaliacao);
                }

                bandasListadas.Add(nomeBanda, novaBanda);
                Console.WriteLine($"\nA banda '{nomeBanda}' do gênero '{generoBanda}' foi registrada com sucesso.");
            }
            else
            {
                Console.WriteLine($"\nA banda '{nomeBanda}' já foi registrada.");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
            ExibirMenu();
        }

        static void ListarBandas()
        {
            Console.Clear();
            TitulosOpcoes("LISTANDO BANDAS REGISTRADAS");

            if (bandasListadas.Count == 0)
            {
                Console.WriteLine("Nenhuma banda foi registrada.");
            }
            else
            {
                foreach (var banda in bandasListadas.Values)
                {
                    double media = banda.ObterMediaAvaliacoes();
                    Console.WriteLine($"Nome: {banda.Nome}, Gênero: {banda.Genero}, Média de Avaliações: {media:F2}");
                }
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
            ExibirMenu();
        }

        static void TitulosOpcoes(string titulo)
        {
            int quantidadeLetras = titulo.Length;
            string asteriscos = string.Empty.PadLeft(quantidadeLetras, '*');
            Console.WriteLine(asteriscos);
            Console.WriteLine(titulo);
            Console.WriteLine(asteriscos + "\n");
        }

        static void ListarMediaBandaEspecifica()
        {
            Console.Clear();
            TitulosOpcoes("LISTANDO MÉDIA DE UMA BANDA ESPECÍFICA");

            Console.WriteLine("\nDigite o nome da banda que deseja ver a média: ");
            string nomeBanda = Console.ReadLine();
            bandasListadas.TryGetValue(nomeBanda, out Banda banda);
            if (banda != null)
            {
                double media = banda.ObterMediaAvaliacoes();
                Console.WriteLine($"Nome: {banda.Nome}, Gênero: {banda.Genero}, Média de Avaliações: {media:F2}");
            }
            else
            {
                Console.WriteLine("Banda não encontrada.");
            }
        }

        static void ExibirMenu()
        {
            ExibirLogo();

            Console.WriteLine("\n[1] Registrar uma banda");
            Console.WriteLine("[2] Listar bandas");
            Console.WriteLine("[3] Listar média de uma banda específica");
            Console.WriteLine("[4] Sair");
            Console.Write("\nDigite a opção desejada: ");

            string opcaoEscolhida = Console.ReadLine();

            if (int.TryParse(opcaoEscolhida, out int opcao))
            {
                switch (opcao)
                {
                    case 1:
                        RegistrarBandas();
                        break;
                    case 2:
                        ListarBandas();
                        break;
                    case 3:
                        ListarMediaBandaEspecifica();
                        break;
                    case 4:
                        Console.WriteLine("Saindo...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Pressione qualquer tecla para tentar novamente...");
                Console.ReadKey();
                Console.Clear();
                ExibirMenu();
            }
        }
    }
}
