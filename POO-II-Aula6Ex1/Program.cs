using POO_II_Aula6Ex1.Controles;

namespace POO_II_Aula6Ex1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string path = @"c:\temp";
            string pathDicionario = path + @"\dicionario.csv";

            try
            {
                if (!Directory.Exists(path))
                {
                    DirectoryInfo di = Directory.CreateDirectory(path);
                }

                int opcao = 0;

                do
                {
                    Tela.Tela.Menu();
                    int.TryParse(Console.ReadLine(), out opcao);
                    switch (opcao)
                    {
                        case 1:
                            Registros.PesquisarTermos(pathDicionario);
                            break;
                        case 2:
                            Registros.CadastrarNovoTermo(pathDicionario);
                            break;
                        case 3:
                            break;
                        default:
                            Console.WriteLine("\nOpção inválida");
                            Console.WriteLine("\nPressione qualquer tecla para continuar.");
                            Console.ReadKey(true);
                            break;

                    }
                } while (opcao != 3);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Não foi possível criar a pasta {path}", e.ToString());
                Console.WriteLine($"Erro encontrado: {e.ToString()}");
            }
        }
    }
}