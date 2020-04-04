using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;
using Xadrez.tabuleiro.Enum;
using Xadrez.JogodeXadrez;
namespace Xadrez
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i=0;i<tabuleiro.Linhas;i++)
            {
                Console.Write(8 - i + " ");
                for (int j=0;j<tabuleiro.Colunas; j++)
                {
    
                    
                        Tela.ImprimirPeca(tabuleiro.Peca(i, j));
                        
                    
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
            
        }
        public static void ImprimirPartida(PartidadeXadrez partidadeXadrez)
        {
            Console.Clear();
            Tela.ImprimirTabuleiro(partidadeXadrez.Tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(partidadeXadrez);
            Console.WriteLine("Turno: " + partidadeXadrez.Turno);
            Console.WriteLine("Aguardando Jogada: " + partidadeXadrez.JogadorAtual);
        }

        public static void ImprimirPecasCapturadas(PartidadeXadrez partidadeXadrez)
        {
            Console.WriteLine("Peças Capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partidadeXadrez.PecasCapturadas(Cor.Branco));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partidadeXadrez.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();

        }
        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca x in conjunto)
            {
                Console.Write(x+" ");
            }
            Console.Write("]");
        }
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                ConsoleColor fundoOriginal = Console.BackgroundColor;
                ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {

                    if(posicoesPossiveis[i,j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else 
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    Tela.ImprimirPeca(tabuleiro.Peca(i, j));


                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");

        }
        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);

        }
        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == Cor.Branco)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
