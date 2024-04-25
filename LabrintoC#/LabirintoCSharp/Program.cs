using System;
using System.Collections.Generic;

namespace LabirintoDoRato
{
    internal class LabirintoDoRato
    {
        private const int limit = 18;

        // Método para mostrar o labirinto no console
        static void mostrarLabirinto(char[,] array, int l, int c)
        {
            // Loop para percorrer o array e mostrar cada caractere
            for (int i = 0; i < l; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < c; j++)
                {
                    Console.Write($"{array[i, j]} ");
                }
            }
            Console.WriteLine("\nProcurando o queijo...");
        }

        // Método para criar o labirinto
        static void criaLabirinto(char[,] meuLab)
        {
            Random random = new Random();
            // Loop para preencher o labirinto com '*' e ' '
            for (int i = 0; i < limit; i++)
            {
                for (int j = 0; j < limit; j++)
                {
                    meuLab[i, j] = random.Next(4) == 1 ? '*' : ' ';
                }
            }

            // Borda do labirinto
            for (int i = 0; i < limit; i++)
            {
                meuLab[0, i] = '*';
                meuLab[limit - 1, i] = '*';
                meuLab[i, 0] = '*';
                meuLab[i, limit - 1] = '*';
            }

            // Posição inicial do queijo
            int x = random.Next(limit);
            int y = random.Next(limit);
            meuLab[x, y] = '#';
        }

        // Método para buscar o queijo
        static void buscarQueijo(char[,] meuLab, int i, int j)
        {
            Stack<int> pilha = new Stack<int>();
            int IAnterior = 0, JAnterior = 0;

            do
            {
                meuLab[i, j] = 'r'; // 'r' representa o rato
                // Condições para movimentar o rato e empilhar a posição anterior
                if (meuLab[i - 1, j] == 'Q' || meuLab[i + 1, j] == 'Q' || meuLab[i, j + 1] == 'Q' || meuLab[i, j - 1] == 'Q')
                {
                    Console.WriteLine("O rato encontrou o queijo ;) !!!!!!");
                    break;
                }
                else if (meuLab[i, j + 1] == ' ')
                {
                    // Movimento para a direita
                    JAnterior = j;
                    IAnterior = i;
                    pilha.Push(i);
                    pilha.Push(j);
                    j++;
                }
                else if (meuLab[i + 1, j] == ' ')
                {
                    // Movimento para baixo
                    JAnterior = j;
                    IAnterior = i;
                    pilha.Push(i);
                    pilha.Push(j);
                    i++;
                }
                else if (meuLab[i, j - 1] == ' ')
                {
                    // Movimento para a esquerda
                    JAnterior = j;
                    IAnterior = i;
                    pilha.Push(i);
                    pilha.Push(j);
                    j--;
                }
                else if (meuLab[i - 1, j] == ' ')
                {
                    // Movimento para cima
                    JAnterior = j;
                    IAnterior = i;
                    pilha.Push(i);
                    pilha.Push(j);
                    i--;
                }
                // Se não houver espaço vazio ou queijo e a pilha não estiver vazia, volta para a posição anterior
                else if (pilha.Count > 0)
                {
                    JAnterior = j;
                    IAnterior = i;
                    j = pilha.Pop();
                    i = pilha.Pop();
                }
                else
                {
                    Console.WriteLine("Não foi possível encontrar o queijo :(");
                    break;
                }

                // Pausa para visualização
                System.Threading.Thread.Sleep(300);
                Console.Clear();//limpa console
                mostrarLabirinto(meuLab, limit, limit);
                meuLab[IAnterior, JAnterior] = '.';
            } while (meuLab[i, j] != 'Q');
        }

        public static void Main(String[] args)
        {
            char[,] labirinto = new char[limit, limit];
            criaLabirinto(labirinto);
            mostrarLabirinto(labirinto, limit, limit);
            buscarQueijo(labirinto, 1, 1);
            Console.ReadKey();
        }
    }
}