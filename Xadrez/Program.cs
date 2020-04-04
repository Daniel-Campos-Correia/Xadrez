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
                Console.WriteLine("\nTurno: "+partidadeXadrez.Turno);
                Console.WriteLine("Aguardando Jogada: "+partidadeXadrez.JogadorAtual);

                while (!partidadeXadrez.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partidadeXadrez.Tab);
                        Console.WriteLine("\nTurno: " + partidadeXadrez.Turno);
                        Console.WriteLine("Aguardando Jogada: " + partidadeXadrez.JogadorAtual);
                        Console.Write("\nOrigem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                        partidadeXadrez.ValidarPosicaodeOrigem(origem);

                        bool[,] posicoesPossiveis = partidadeXadrez.Tab.Peca(origem).MovimentosPossiveis();
                        Console.Clear();
                        Tela.ImprimirTabuleiro(partidadeXadrez.Tab, posicoesPossiveis);


                        Console.Write("\nDestino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                        partidadeXadrez.validarPosicaodeDestino(origem,destino);

                        partidadeXadrez.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();

                    }
                }
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
