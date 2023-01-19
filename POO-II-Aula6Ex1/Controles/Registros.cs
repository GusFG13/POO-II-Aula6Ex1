using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POO_II_Aula6Ex1.Controles
{
    internal class Registros
    {
        public static void PesquisarTermos(string path)
        {
            string termo;
            do
            {
                Console.Clear();
                Console.Write("Digite o termo a pesquisar: ");
                termo = Console.ReadLine();
            } while (String.IsNullOrEmpty(termo));
            List<string> registrosEncontrados = BuscaRegistros(path, termo);

            if (registrosEncontrados.Count > 0)
            {
                Console.Clear();
                Console.WriteLine($"\nTermo(s) encontrado(s):");
                
                registrosEncontrados.Sort();
                foreach (string reg in registrosEncontrados)
                {
                    Console.WriteLine();
                    string[] aux = reg.Split(';');
                    Console.WriteLine($"-> {aux[0].ToUpper()}");

                    for (int i = 1; i < aux.Length; i++)
                    {
                        Console.WriteLine($"   - {aux[i]}");
                    }
                }

            }
            else
            {
                Console.WriteLine("\nNenhum termo encontrado");
            }
            Console.WriteLine();
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
            Console.ReadKey(true);
        }

        private static List<string> BuscaRegistros(string path, string termo)
        {
            List<string> termosCorresp = new List<string>();
            if (File.Exists(path))
            {
                // Open the file to read from.
                using (StreamReader sr = File.OpenText(path))
                {
                    string s = "";
                    string aux;
                    while ((s = sr.ReadLine()) != null)
                    {
                        aux = s.Split(';')[0].ToUpper();
                        if (aux.Contains(termo.ToUpper()))
                        {
                            termosCorresp.Add(s);

                        }
                    }
                    sr.Close();
                }
            }

            return termosCorresp;

        }

        public static void CadastrarNovoTermo(string path)
        {
            string termo;
            List<string> significados = new List<string>();
            int cont = 1;
            string significado = "";
            char resposta;
            do
            {
                Console.Clear();
                Console.Write("Digite o termo a cadastrar: ");
                termo = Console.ReadLine();
            } while (String.IsNullOrEmpty(termo));


            do
            {
                Console.Clear();
                Console.WriteLine($"Digite os significados do termo {termo.ToUpper()}");
                Console.WriteLine($"Se houver mais de um, digite apenas 1 por linha. Digite \"FIM\" para terminar");
                Console.WriteLine();

                Console.Write($"Significado {cont}: ");
                significado = Console.ReadLine();
                if (significado != "FIM" && !String.IsNullOrEmpty(significado))
                {
                    do
                    {
                        Console.Clear();
                        Console.WriteLine($"\nSignificado {cont}: {significado}");
                        Console.WriteLine("\nDeseja realmente inserir esse significado (s/n)?");
                        char.TryParse(Console.ReadLine(), out resposta);
                    } while (resposta != 's' && resposta != 'n');
                    if (resposta == 's')
                    {
                        cont++;
                        significados.Add(significado.Replace(';', ','));
                    }
                }

            } while (significado != "FIM");


            if (significados.Count > 0)
            {
                Console.Clear();
                Console.WriteLine($"\nO novo registro inserido será:");
                Console.WriteLine($" - Termo: {termo}");
                cont = 1;
                Console.WriteLine($"\nSignificado(s):");
                foreach (string sig in significados)
                {
                    Console.WriteLine($" - {cont}: {sig};");
                    cont++;
                }
                do
                {
                    Console.WriteLine("\nDeseja realmente inserir esse registro (s/n)?");
                    char.TryParse(Console.ReadLine(), out resposta);
                } while (resposta != 's' && resposta != 'n');
                if (resposta == 's')
                {
                    StringBuilder termoCompleto = new StringBuilder();
                    termoCompleto.Append(termo.Replace(';', ','));
                    foreach (string sig in significados)
                    {
                        termoCompleto.Append(";" + sig);
                    }
                    GravarNovoTermo(path, termoCompleto.ToString());
                    Console.WriteLine("Registro adicionado!");
                }
                else
                {
                    Console.WriteLine("Operação cancelada!");
                }
            }
            else
            {
                Console.WriteLine("Operação cancelada. Nenhum significado para o termo foi digitado.");
            }
            Console.WriteLine("Pressione qualquer tecla para voltar ao menu.");
            Console.ReadKey(true);

        }

        private static void GravarNovoTermo(string path, string dados)
        {
            // This text is added only once to the file.
            if (File.Exists(path))
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(dados);
                    sw.Close();
                }
            }
            else
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(dados);
                    sw.Close();
                }
            }
        }
    }
}
