using System;
using Xadrez.tabuleiro;
using Xadrez.JogodeXadrez;
using Xadrez.tabuleiro.Enum;



namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez pos = new PosicaoXadrez('a', 1);
            Console.WriteLine(pos);
            Console.WriteLine(pos.ToPosicao());

            try
            {
                PartidadeXadrez partidadeXadrez = new PartidadeXadrez();
                Tela.ImprimirTabuleiro(partidadeXadrez.Tab);

                while (!partidadeXadrez.Terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partidadeXadrez.Tab);
                    Console.Write("\nOrigem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();

                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                    partidadeXadrez.ExecutaMovimento(origem, destino);
                }
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
